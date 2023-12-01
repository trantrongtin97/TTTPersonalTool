using Blazored.LocalStorage;
using Blazored.Toast;
using Microsoft.AspNetCore.Components.Authorization;
using TTT.PersonalTool.Shared;
using TTT.PersonalTool.Shared.Const;
using TTT.PersonalTool.Shared.Logging;
using TTT.PersonalTool.Shared.Services;
using TTT.PersonalTool.Shared.ViewModels;
using TTT.PersonalTool.Shared.ViewModels.Interfaces;

namespace TTT.PersonalTool.Client.Extensions
{
    public static class ServiceExtension
    {
        public static IServiceCollection TTTRegisterCore(this IServiceCollection services,
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

            return services;
        }

        public static IServiceCollection TTTRegisterLogClient(this IServiceCollection services ,ApplicationSettings applicationSettings)
        {
            services.AddLogging(logging => logging.SetMinimumLevel(LogLevel.Error));
            services.AddSingleton<LogQueue>();
            services.AddSingleton<LogReader>();
            services.AddSingleton<LogWriter>();
            services.AddSingleton<ILoggerProvider, ApplicationLoggerProvider>();
            services.AddHttpClient("LoggerJob", c => c.BaseAddress = new Uri(applicationSettings.BaseAddress));
            services.AddSingleton<LoggerJob>();

            return services;
        }

        public static IServiceCollection TTTRegisterClient(this IServiceCollection services, ApplicationSettings applicationSettings,string strNamespace)
        {
            // configuring http clients
            services.AddScoped(_ => new HttpClient { BaseAddress = new Uri(applicationSettings.BaseAddress) });

            // transactional named http clients
            var clientConfigurator = void (HttpClient client) => client.BaseAddress = new Uri(applicationSettings.BaseAddress);

            // System client
            services.AddHttpClient<IProfileViewModel, ProfileViewModel>("ProfileViewModelClient", clientConfigurator);
            services.AddHttpClient<ISettingsViewModel, SettingsViewModel>("SettingsViewModelClient", clientConfigurator);
            services.AddHttpClient<ILoginViewModel, LoginViewModel>("LoginViewModelClient", clientConfigurator);
            services.AddHttpClient<IRegisterViewModel, RegisterViewModel>("RegisterViewModelClient", clientConfigurator);
            services.AddHttpClient<IAssignRolesViewModel, AssignRolesViewModel>("AssignRolesViewModel", clientConfigurator);

            // Plugin client. Add more
            services.AddHttpClient<IItemViewModel, ItemViewModel>("ItemViewModelClient", clientConfigurator);

            return services;
        }

        public static IServiceCollection TTTRegisterPolicy(this IServiceCollection services)
        {
            services.AddAuthorizationCore(options =>
            {
                options.AddPolicy(nameof(TTTPermissions.Policy_LvFull), policy =>
                    policy.RequireRole(TTTPermissions.Policy_LvFull));

                options.AddPolicy(nameof(TTTPermissions.Policy_LvEmployee), policy =>
                      policy.RequireRole(TTTPermissions.Policy_LvEmployee));

                options.AddPolicy(nameof(TTTPermissions.Policy_LvManager), policy =>
                      policy.RequireRole(TTTPermissions.Policy_LvManager));

                options.AddPolicy(nameof(TTTPermissions.Policy_LvAdmin), policy =>
                      policy.RequireRole(TTTPermissions.Policy_LvAdmin));
            });
            return services;
        }

    }
}
