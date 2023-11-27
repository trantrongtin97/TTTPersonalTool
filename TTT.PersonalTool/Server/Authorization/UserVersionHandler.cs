using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using TTT.PersonalTool.Shared.IRepositories;

namespace TTT.PersonalTool.Server.Authorization
{
    public class UserVersionHandler : AuthorizationHandler<VersionRequirement>
    {
        private readonly IUserRepository _userRepository;
        public UserVersionHandler(IUserRepository userRepository)
        {
            this._userRepository = userRepository;
        }
        protected override async Task HandleRequirementAsync(
            AuthorizationHandlerContext context, VersionRequirement requirement)
        {
            var versionClaim = context.User.FindFirst(
                c => c.Type == ClaimTypes.Version);
            var userIdClaim = context.User.FindFirst(
                c => c.Type == ClaimTypes.NameIdentifier);

            if ((versionClaim is null) || (userIdClaim is null))
            {
                context.Fail();
                return;
            }
            int.TryParse(userIdClaim.Value, out int id);
            var user = await _userRepository.GetByIdAsync(id);
            if (user.Version == versionClaim.Value)
            {
                context.Succeed(requirement);
                return;
            }
            context.Fail();
            return ;
        }
    }
}
