using TTT.Framework.ServiceExtentions;
using TTT.PersonalTool.Shared.Models;
using TTT.PersonalTool.Shared.Services;
using TTT.PersonalTool.Shared.ViewModels.Interfaces;

namespace TTT.PersonalTool.Shared.ViewModels
{
    public class AssignRolesViewModel : IAssignRolesViewModel
    {
        public IEnumerable<User> AllUsers { get; private set; } = new List<User>();
        public IEnumerable<string> AllRoles { get; private set; } = new List<string>();

        private readonly HttpClient _httpClient;
        private readonly IAccessTokenService _accessTokenService;

        public AssignRolesViewModel(HttpClient httpClient,
            IAccessTokenService accessTokenService)
        {
            _httpClient = httpClient;
            _accessTokenService = accessTokenService;
        }

        public async Task LoadAllUsers()
        {
            var jwtToken = await _accessTokenService.GetAccessTokenAsync("jwt_token");
            AllUsers = await _httpClient.GetAsync<List<User>>("user/getallusers", jwtToken);
        }

        public async Task AssignRole(int userId, string role)
        {
            var jwtToken = await _accessTokenService.GetAccessTokenAsync("jwt_token");
            await _httpClient.PutAsync<int>("user/assignrole", new User { Id = userId, Role = role,Username="t",Password = "t" }, jwtToken);
        }

        public async Task DeleteUser(int userId)
        {
            var jwtToken = await _accessTokenService.GetAccessTokenAsync("jwt_token");
            int result = await _httpClient.DeleteAsync($"user/deleteuser/{userId}", jwtToken);
        }

        public async Task LoadAllRole()
        {
            var jwtToken = await _accessTokenService.GetAccessTokenAsync("jwt_token");
            AllRoles = await _httpClient.GetAsync<List<string>>("user/getallroles", jwtToken);
        }
    }
}
