using TTT.PersonalTool.Shared.Models;

namespace TTT.PersonalTool.Server.Services.IServices
{
    public interface ICoreSystermTTT
    {
        public Task<string> GenerateJwtToken(User user);
        public Task<User> GetUserByJWT(string jwtToken);
        public Task<User> GetRequestUser();
    }
}
