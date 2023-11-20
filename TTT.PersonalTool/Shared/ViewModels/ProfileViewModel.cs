using TTT.PersonalTool.Shared.Models;
using TTT.PersonalTool.Shared.Attributes;
using TTT.PersonalTool.Shared.Extensions;
using TTT.PersonalTool.Shared.Services;
using TTT.PersonalTool.Shared.ViewModels.Interfaces;

namespace TTT.PersonalTool.Shared.ViewModels
{
    public class ProfileViewModel : IProfileViewModel
    {
        public int Id { get; set; }

        [TTTStringValidator(Requied = true, MaximumSize = DefineFieldValue.String_Lenght_200, Display = "First Name")]
        public string? FirstName { get; set; }

        [TTTStringValidator(Requied = true, MaximumSize = DefineFieldValue.String_Lenght_200, Display = "Last Name")]
        public string? LastName { get; set; }

        [TTTStringValidator(Requied = true, MaximumSize = DefineFieldValue.String_Lenght_50, Display = "Username")]
        public string Username { get; set; }
        public string Password { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? ProfilePicDataUrl { get; set; }

        private readonly HttpClient _httpClient;
        private readonly IAccessTokenService _accessTokenService;

        public ProfileViewModel()
        {
        }

        public ProfileViewModel(HttpClient httpClient,
            IAccessTokenService accessTokenService)
        {
            _httpClient = httpClient;
            _accessTokenService = accessTokenService;
        }

        public async Task UpdateProfile()
        {
            var jwtToken = await _accessTokenService.GetAccessTokenAsync("jwt_token");
            _ = await _httpClient.PutAsync<User>($"profile/updateprofile/{Id}", this, jwtToken);
        }

        public async Task GetProfile()
        {
            var jwtToken = await _accessTokenService.GetAccessTokenAsync("jwt_token");
            ProfileViewModel user = await _httpClient.GetAsync<User>($"profile/getprofile/{Id}", jwtToken);
            LoadCurrentObject(user);
        }

        private void LoadCurrentObject(IProfileViewModel profileViewModel)
        {
            FirstName = profileViewModel.FirstName;
            LastName = profileViewModel.LastName;
            Username = profileViewModel.Username;
            Password = profileViewModel.Password;
            DateOfBirth = profileViewModel.DateOfBirth;
            ProfilePicDataUrl = profileViewModel.ProfilePicDataUrl;
            //add more fields
        }

        public static implicit operator ProfileViewModel(User user) =>
            new()
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Username = user.Username,
                Password = user.Password,
                DateOfBirth = user.DateOfBirth,
                Id = user.Id
            };

        public static implicit operator User(ProfileViewModel profileViewModel) =>
            new()
            {
                FirstName = profileViewModel.FirstName,
                LastName = profileViewModel.LastName,
                Username = profileViewModel.Username,
                Password = "",
                DateOfBirth = profileViewModel.DateOfBirth,
                Id = profileViewModel.Id
            };
    }
}
