using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TTT.PersonalTool.Contracts.IRepositories;
using TTT.PersonalTool.Server.Services.IServices;
using TTT.PersonalTool.Shared;
using TTT.PersonalTool.Shared.Const;
using TTT.PersonalTool.Shared.Enums;
using TTT.PersonalTool.Shared.Models;

namespace TTT.PersonalTool.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;
        private readonly ITTTSercurity _sercurity;

        public UserController(ILogger<UserController> logger,
                                ITTTSercurity sercurity,
                                IConfiguration configuration,
                                IUserRepository userRepository)
        {
            this._logger = logger;
            this._sercurity = sercurity;
            this._configuration = configuration;
            this._userRepository = userRepository;
        }

        [HttpPost("registeruser")]
        public async Task<ActionResult<UserState>> RegisterUser(User user)
        {
            var usernameExists = await _userRepository.GetByUsernameAsync(user.Username);
            if (usernameExists == null)
            {
                user.Password = _sercurity.EncryptAES(user.Password);
                user.Role = TTTPermissions.Member;
                user.CreatedDate = DateTime.UtcNow;
                user.Theme = StUserTheme.Light;
                await _userRepository.InsertAsync(user, true);
                return Ok(UserState.Created);
            }
            return Ok(UserState.Existed);
        }


        [HttpPost("authenticatejwt")]
        public async Task<ActionResult<AuthenticationResponse>> AuthenticateJWT(AuthenticationRequest authenticationRequest)
        {
            string token = string.Empty;
            authenticationRequest.Password = _sercurity.EncryptAES(authenticationRequest.Password);

            User? loggedInUser = await _userRepository.VerifyUsernamePasswordAsync(authenticationRequest.Username, authenticationRequest.Password);
            if (loggedInUser == null)
            {
                token = string.Empty;
            }
            else
            {
                token = GenerateJwtToken(loggedInUser);
            }
            return await Task.FromResult(new AuthenticationResponse() { Token = token });
        }

        [HttpPost("getuserbyjwt")]
        public async Task<ActionResult<User>> GetUserByJWT([FromBody] string jwtToken)
        {
            try
            {
                string secretKey = _configuration["JWTSettings:SecretKey"] ?? "";
                var key = Encoding.ASCII.GetBytes(secretKey);

                var tokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
                var tokenHandler = new JwtSecurityTokenHandler();
                SecurityToken securityToken;

                var principle = tokenHandler.ValidateToken(jwtToken, tokenValidationParameters, out securityToken);
                var jwtSecurityToken = (JwtSecurityToken)securityToken;

                if (jwtSecurityToken != null
                    && jwtSecurityToken.ValidTo > DateTime.Now
                    && jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                {
                    var userId = principle.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                    User rs = await GetUserByUserID(Convert.ToInt32(userId));
                    return ToCleanData(rs);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception : " + ex.Message);
            }
            return BadRequest();
        }

        [Authorize(Roles = TTTPermissions.Admin)]
        [HttpGet("getallusers")]
        public async Task<ActionResult<List<User>>> GetAllUsers()
        {
            return await _userRepository.GetListAsync();
        }

        [Authorize(Roles = TTTPermissions.Admin)]
        [HttpPut("assignrole")]
        public async Task<ActionResult<int>> AssignRole([FromBody] User user)
        {
            var a = await HttpContext.GetTokenAsync("access_token");
            User? userToUpdate = await _userRepository.GetByIdAsync(user.Id);
            if (userToUpdate != null)
            {
                userToUpdate.Role = user.Role;
                return await _userRepository.GetContext().SaveChangesAsync();
            }
            return BadRequest();
        }

        [Authorize(Roles = TTTPermissions.Admin)]
        [HttpDelete("deleteuser/{userId}")]
        public async Task<ActionResult<int>> DeleteUser(int userId)
        {
            await _userRepository.DeleteAsync(new User() { Id = userId }, true);
            return Ok(1);
        }

        protected async Task<User> GetUserByUserID(int userId)
        {
            return await _userRepository.GetByIdAsync(userId)??new User();
        }

        protected string GenerateJwtToken(User user)
        {
            string secretKey = _configuration["JWTSettings:SecretKey"] ?? "";
            var key = Encoding.ASCII.GetBytes(secretKey);

            var claimEmail = new Claim(ClaimTypes.Email, user.Username);
            var claimNameIdentifier = new Claim(ClaimTypes.NameIdentifier, user.Id.ToString());
            var claimRole = new Claim(ClaimTypes.Role, user.Role == null ? "" : user.Role);

            var claimsIdentity = new ClaimsIdentity(new[] { claimEmail, claimNameIdentifier, claimRole }, "serverAuth");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = claimsIdentity,
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        private static User ToCleanData(User user) =>
            new User
            {
                Id = user.Id,
                Username = user.Username,
                Password = "",
                FirstName = user.FirstName,
                LastName = user.LastName,
                ProfilePictureUrl = user.ProfilePictureUrl,
                Role = user.Role,
                Theme = user.Theme,
                DateOfBirth = user.DateOfBirth,
                CreatedDate = user.CreatedDate,
            };
            
    }
}
