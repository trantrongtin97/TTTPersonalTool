using System.ComponentModel.DataAnnotations;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using TTT.PersonalTool.Shared.Models;
using TTT.PersonalTool.Shared.ViewModels.Interfaces;

namespace TTT.PersonalTool.Shared.ViewModels
{
    public class LoginViewModel : ILoginViewModel
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        public bool RememberMe { get; set; }

        private readonly HttpClient _httpClient;

        public LoginViewModel()
        {
        }
        public LoginViewModel(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public Task LoginUser() =>
            _httpClient.PostAsJsonAsync<User>($"user/loginuser?isPersistent={RememberMe}", this);

        public async Task<AuthenticationResponse> AuthenticateJWT()
        {
            var authenticationRequest = new AuthenticationRequest
            {
                Username = Username,
                Password = Password,
            };

            var httpMessageResponse = await _httpClient.PostAsJsonAsync("user/authenticatejwt", authenticationRequest);
            return await httpMessageResponse.Content.ReadFromJsonAsync<AuthenticationResponse>();
        }

        public async Task<User> GetUserByJWTAsync(string jwtToken)
        {
            try
            {
                using var requestMessage = new HttpRequestMessage(HttpMethod.Post, "user/getuserbyjwt")
                {
                    Content = new StringContent(jwtToken)
                    {
                        Headers =
                        {
                            ContentType = new MediaTypeHeaderValue("application/json")
                        }
                    }
                };

                var response = await _httpClient.SendAsync(requestMessage);

                var returnedUser = await response.Content.ReadFromJsonAsync<User>();
                if (returnedUser != null) return await Task.FromResult(returnedUser);
                else return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.GetType());
                return null;
            }
        }

        public static implicit operator LoginViewModel(User user) =>
            new()
            {
                Username = user.Username,
                Password = user.Password
            };

        public static implicit operator User(LoginViewModel loginViewModel) =>
            new()
            {
                Username = loginViewModel.Username,
                Password = loginViewModel.Password
            };
    }
}
