using System.Security.Claims;
using TTT.PersonalTool.Server.Services.IServices;

namespace TTT.PersonalTool.Server.Services
{
    public class ControlDataByTenant : IControlDataProvider
    {
        public ControlDataByTenant(IHttpContextAccessor accessor)
        {
            TenantCode = accessor.HttpContext?
                .User.Claims.SingleOrDefault(x =>
                    x.Type == ClaimTypes.Surname)?.Value;
        }

        public string TenantCode { get; set; }
    }
}
