using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using TTT.PersonalTool.Shared.Enums;
using TTT.PersonalTool.Shared.Models;
using TTT.PersonalTool.Shared.Services;
using TTT.PersonalTool.Shared.ViewModels.Interfaces;
using TTT.Framework.Attributes;
using TTT.Framework.ServiceExtentions;
using TTT.PersonalTool.Shared.Dtos;

namespace TTT.PersonalTool.Shared.ViewModels
{
    public class RegisterViewModel : IRegisterViewModel
    {
        [TTTStringValidator(Requied = true,MaximumSize =DefineFieldValue.String_Lenght_200,Display = "First Name")]
        public string FirstName { get; set; }

        [TTTStringValidator(Requied = true, MaximumSize = DefineFieldValue.String_Lenght_200, Display = "Last Name")]
        public string LastName { get; set; }

        [TTTStringValidator(Requied = true, MaximumSize = DefineFieldValue.String_Lenght_50, Display = "Username")]
        public string Username { get; set; }

        [TTTPasswordValidator(Requied = true, MinimumSize = 8, MaximumSize = 20, Display = "Password")]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "Password and Re-enter Password must match")]
        public string ReenterPassword { get; set; }

        [TTTStringValidator(MaximumSize = DefineFieldValue.String_Lenght_500, Display = "Tenant Code")]
        public string TenantCode { get; set; }

        private readonly HttpClient _httpClient;
        private readonly IAccessTokenService _accessTokenService;

        public RegisterViewModel()
        {
        }
        public RegisterViewModel(HttpClient httpClient,
            IAccessTokenService accessTokenService)
        {
            _httpClient = httpClient;
            _accessTokenService = accessTokenService;
        }

        public async Task<UserState> RegisterUser()
        {
            var jwtToken = await _accessTokenService.GetAccessTokenAsync("jwt_token");
            return await _httpClient.PostAsync<UserState>("user/registeruser", this, jwtToken);
        }

        public static implicit operator RegisterViewModel(User user) =>
            new()
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Username = user.Username,
                Password = user.Password
            };

        public static implicit operator RegisterDto(RegisterViewModel registerViewModel) =>
            new()
            {
                FirstName = registerViewModel.FirstName,
                LastName = registerViewModel.LastName,
                Username = registerViewModel.Username,
                Password = registerViewModel.Password,
                TenantCode = registerViewModel.TenantCode
            };
    }
}
