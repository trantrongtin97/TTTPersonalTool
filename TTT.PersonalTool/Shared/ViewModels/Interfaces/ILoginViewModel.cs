using TTT.PersonalTool.Shared.Models;

namespace TTT.PersonalTool.Shared.ViewModels.Interfaces
{
    public interface ILoginViewModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public bool RememberMe { get; set; }
        public Task LoginUser();
        public Task<AuthenticationResponse> AuthenticateJWT();
        public Task<User> GetUserByJWTAsync(string jwtToken);
    }
}
