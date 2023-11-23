using TTT.PersonalTool.Shared.Models;

namespace TTT.PersonalTool.Server.Services.IServices
{
    public interface ICoreSystermTTT
    {
        public Task<string> GenerateJwtToken(User user);
        public Task<int> GetUserIDByJWT(string jwtToken);
        public Task<int> GetIDRequestUser(HttpContext httpContext);
    }
}
