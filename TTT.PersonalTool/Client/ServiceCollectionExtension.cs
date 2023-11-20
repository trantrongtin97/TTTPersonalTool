using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using TTT.PersonalTool.Shared.Services;
using TTT.PersonalTool.Shared.ViewModels.Interfaces;
using TTT.PersonalTool.Shared.ViewModels;
using Blazored.Toast;
using Blazored.LocalStorage;
using TTT.PersonalTool.Shared;
using System.Net.Http;
using TTT.PersonalTool.Shared.Logging;

namespace TTT.PersonalTool.Client.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddPersonalTool(this IServiceCollection services,
            ApplicationSettings applicationSettings)
        {
            // blazored services
            services.AddBlazoredToast();
            services.AddBlazoredLocalStorage();

            // authetication & authorization
            services.AddOptions();
            services.AddAuthorizationCore();
            services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();
            services.AddScoped<IAccessTokenService, WebAppAccessTokenService>();

            // configuring http clients
            services.AddScoped(_ => new HttpClient { BaseAddress = new Uri(applicationSettings.BaseAddress) });

            // transactional named http clients
            var clientConfigurator = void (HttpClient client) => client.BaseAddress = new Uri(applicationSettings.BaseAddress);
            services.AddHttpClient<IProfileViewModel, ProfileViewModel>("ProfileViewModelClient", clientConfigurator);
            services.AddHttpClient<ISettingsViewModel, SettingsViewModel>("SettingsViewModelClient", clientConfigurator);

            // authentication http clients
            services.AddHttpClient<ILoginViewModel, LoginViewModel>("LoginViewModelClient", clientConfigurator);
            services.AddHttpClient<IRegisterViewModel, RegisterViewModel>("RegisterViewModelClient", clientConfigurator);
            services.AddHttpClient<IAssignRolesViewModel, AssignRolesViewModel>("AssignRolesViewModel", clientConfigurator);

            // logging
            services.AddLogging(logging => logging.SetMinimumLevel(LogLevel.Error));
            services.AddSingleton<LogQueue>();
            services.AddSingleton<LogReader>();
            services.AddSingleton<LogWriter>();
            services.AddSingleton<ILoggerProvider, ApplicationLoggerProvider>();
            services.AddHttpClient("LoggerJob", c => c.BaseAddress = new Uri(applicationSettings.BaseAddress));
            services.AddSingleton<LoggerJob>();

            return services;
        }
    }
}
