using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using TTT.Framework.Sercurity;
using TTT.PersonalTool.Shared.IRepositories;
using TTT.PersonalTool.Server.Services.IServices;
using TTT.PersonalTool.Shared;
using TTT.PersonalTool.Shared.Const;
using TTT.PersonalTool.Shared.Dtos;
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
        private readonly ITenantRepository _tenantRepository;
        private readonly IConfiguration _configuration;
        private readonly ITTTSercurity _sercurity;
        private readonly IMapper _mapper;

        public UserController(ILogger<UserController> logger,
                                ITTTSercurity sercurity,
                                IConfiguration configuration,
                                IUserRepository userRepository,
                                ITenantRepository tenantRepository,
                                IMapper mapper,
                                ICoreSystermTTT systermTTT)
        {
            this._logger = logger;
            this._sercurity = sercurity;
            this._configuration = configuration;
            this._userRepository = userRepository;
            this._tenantRepository = tenantRepository;
            this._systermTTT = systermTTT;
            this._mapper = mapper;
        }

        [AllowAnonymous]
        [HttpPost("registeruser")]
        public async Task<ActionResult<UserState>> RegisterUser(RegisterDto registerDto)
        {
            bool fla = false;
            User user = _mapper.Map<User>(registerDto);
            var usernameExists = await _userRepository.GetByUsernameAsync(user.Username);
            if (usernameExists == null)
            {
                if (string.IsNullOrEmpty(user.TenantCode))
                {
                    fla = true;
                    user.TenantCode = Guid.NewGuid().ToString();
                }
                user.Password = _sercurity.EncryptAES(user.Password);
                user.Role = TTTPermissions.Member;
                user.CreatedDate = DateTime.UtcNow;
                user.Theme = StUserTheme.Light;
                user.Version = $"v1.0";
                var userIns = await _userRepository.InsertAsync(user, true);
                if (fla  && await _tenantRepository.GetByCode(user.TenantCode) == 0)
                {
                    await _tenantRepository.InsertAsync(new Tenant()
                    {
                        Code = user.TenantCode,
                        UserID = userIns.Id
                    }, true);
                }
                return Ok(UserState.Created);
            }
            return Ok(UserState.Existed);
        }


        [AllowAnonymous]
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

        [AllowAnonymous]
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

        [Authorize(Policy = nameof(TTTPermissions.Policy_LvAdmin))]
        [HttpGet("getallusers")]
        public async Task<ActionResult<List<User>>> GetAllUsers()
        {
            return await _userRepository.GetListAsync();
        }

        [Authorize(Policy = nameof(TTTPermissions.Policy_LvAdmin))]
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

        [Authorize(Policy = nameof(TTTPermissions.Policy_LvAdmin))]
        [HttpDelete("deleteuser/{userId}")]
        public async Task<ActionResult<int>> DeleteUser(int userId)
        {
            await _userRepository.DeleteAsync(new User() { Id = userId }, true);
            return Ok(1);
        }

        private static User ToCleanData(User user) =>
            new ()
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
                CreatedDate = user.CreatedDate
            };
            
    }
}
