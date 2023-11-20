using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using TTT.PersonalTool.Contracts.IRepositories;
using TTT.PersonalTool.Shared.Const;
using TTT.PersonalTool.Shared.Models;

namespace TTT.PersonalTool.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class SettingsController : ControllerBase
    {
        private readonly ILogger<SettingsController> logger;
        private readonly IUserRepository _userRepository;

        public SettingsController(ILogger<SettingsController> logger, IUserRepository userRepository)
        {
            this.logger = logger;
            this._userRepository = userRepository;
        }

        [HttpPut("updatetheme/{userId}")]
        public async Task<ActionResult<HttpStatusCode>> UpdateTheme(string userId, User user)
        {
            
            if (int.TryParse(userId, out var rs))
            {
                User? userToUpdate = await _userRepository.GetByIdAsync(rs);
                if (userToUpdate == null) return HttpStatusCode.BadRequest;
                userToUpdate.Theme = (user.Theme == StUserTheme.Dark) ? StUserTheme.Dark : StUserTheme.Light;

                await _userRepository.GetContext().SaveChangesAsync();
                return HttpStatusCode.OK;
            }
            return HttpStatusCode.BadRequest;

        }
    }
}
