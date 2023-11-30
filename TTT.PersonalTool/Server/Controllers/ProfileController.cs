using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TTT.PersonalTool.Shared.Const;
using TTT.PersonalTool.Shared.IRepositories;
using TTT.PersonalTool.Shared.Models;

namespace TTT.PersonalTool.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class ProfileController : ControllerBase
    {
        private readonly ILogger<ProfileController> logger;
        private readonly IUserRepository _userRepository;

        public ProfileController(ILogger<ProfileController> logger, IUserRepository userRepository)
        {
            this.logger = logger;
            this._userRepository = userRepository;
        }

        [HttpPut("updateprofile/{userId}")]
        [Authorize(Policy = nameof(TTTPermissions.Policy_LvFull))]
        public async Task<ActionResult<User>> UpdateProfile(int userId, [FromBody] User user)
        {
            try
            {
                User? userToUpdate = await _userRepository.GetByIdAsync(userId);
                if (userToUpdate != null)
                {
                    userToUpdate.FirstName = user.FirstName;
                    userToUpdate.LastName = user.LastName;
                    userToUpdate.Username = user.Username;
                    userToUpdate.DateOfBirth = user.DateOfBirth;
                    await _userRepository.GetContext().SaveChangesAsync();
                }
                return user;
            }
            catch
            {
                throw;
            }
        }

        [HttpGet("getprofile/{userId}")]
        [Authorize(Policy = nameof(TTTPermissions.Policy_LvFull))]
        public async Task<ActionResult<User>> GetProfile(int userId)
        {
            try
            {
                User? user = await _userRepository.GetByIdAsync(userId);
                if (user == null) return new User();
                return ToCleanData(user);
            }
            catch
            {
                throw;
            }
        }

        [HttpGet("DownloadServerFile")]
        [Authorize(Policy = nameof(TTTPermissions.Policy_LvFull))]
        public async Task<ActionResult<string>> DownloadServerFile()
        {
            try
            {
                var filePath = @"C:\Data\";
                using (var fileInput = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                {
                    MemoryStream memoryStream = new MemoryStream();
                    await fileInput.CopyToAsync(memoryStream);

                    var buffer = memoryStream.ToArray();
                    return Convert.ToBase64String(buffer);
                }
            }
            catch
            {
                throw;
            }
        }

        private static User ToCleanData(User user) =>
         new()
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
