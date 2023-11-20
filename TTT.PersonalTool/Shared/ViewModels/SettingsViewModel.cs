using System.Net;
using TTT.PersonalTool.Shared.Const;
using TTT.PersonalTool.Shared.Extensions;
using TTT.PersonalTool.Shared.Models;
using TTT.PersonalTool.Shared.Services;
using TTT.PersonalTool.Shared.ViewModels.Interfaces;

namespace TTT.PersonalTool.Shared.ViewModels
{
    public class SettingsViewModel : ISettingsViewModel
    {
        public int Id { get; set; }
        public bool Theme { get; set; }

        private readonly HttpClient _httpClient;
        private readonly IAccessTokenService _accessTokenService;


        public SettingsViewModel()
        {
        }

        public SettingsViewModel(HttpClient httpClient,
            IAccessTokenService accessTokenService)
        {
            _httpClient = httpClient;
            _accessTokenService = accessTokenService;
        }

        public async Task GetProfile()
        {
            var jwtToken = await _accessTokenService.GetAccessTokenAsync("jwt_token");
            var user = await _httpClient.GetAsync<User>($"profile/getprofile/{Id}", jwtToken);
            LoadCurrentObject(user);
        }

        public async Task UpdateTheme()
        {
            var jwtToken = await _accessTokenService.GetAccessTokenAsync("jwt_token");
            User user = this;
            await _httpClient.PutAsync<HttpStatusCode>($"settings/updatetheme/{Id}", user, jwtToken);
        }

        public async Task UpdateNotifications()
        {
            var jwtToken = await _accessTokenService.GetAccessTokenAsync("jwt_token");
            User user = this;
            await _httpClient.PutAsync<User>($"settings/updatenotifications/{Id}", user, jwtToken);
        }

        private void LoadCurrentObject(SettingsViewModel settingsViewModel)
        {
            Theme = settingsViewModel.Theme;
        }

        public static implicit operator SettingsViewModel(User user) =>
            new()
            {
                Theme = (user.Theme != null && user.Theme == StUserTheme.Dark)? true : false
            };

        public static implicit operator User(SettingsViewModel settingsViewModel) =>
            new()
            {
                Username = "anou",
                Password = "anou",
                Theme = (settingsViewModel.Theme) ? StUserTheme.Dark : StUserTheme.Light
            };
    }
}
