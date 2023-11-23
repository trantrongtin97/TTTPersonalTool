using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TTT.Framework.Sercurity;
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
        private readonly ICoreSystermTTT _systermTTT;
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;
        private readonly ITTTSercurity _sercurity;

        public UserController(ILogger<UserController> logger,
                                ITTTSercurity sercurity,
                                IConfiguration configuration,
                                IUserRepository userRepository,
                                ICoreSystermTTT systermTTT)
        {
            this._logger = logger;
            this._sercurity = sercurity;
            this._configuration = configuration;
            this._userRepository = userRepository;
            this._systermTTT = systermTTT;
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
                token = await _systermTTT.GenerateJwtToken(loggedInUser);
            }
            return await Task.FromResult(new AuthenticationResponse() { Token = token });
        }

        [HttpPost("getuserbyjwt")]
        public async Task<ActionResult<User>> GetUserByJWT([FromBody] string jwtToken)
        {
            try
            {
                int userid = await _systermTTT.GetUserIDByJWT(jwtToken);
                return ToCleanData(await _userRepository.GetByIdAsync(userid) ?? new User());
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

        private static User ToCleanData(User user) =>
            new User
            {
                Id = user.Id,
                Username = user.Username,
                Password = "xxx",
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
