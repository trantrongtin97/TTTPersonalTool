using TTT.Framework.Sercurity;
using TTT.PersonalTool.Contracts.IRepositories;
using TTT.PersonalTool.Server.Logging;
using TTT.PersonalTool.Server.Repositories;
using TTT.PersonalTool.Server.Services;
using TTT.PersonalTool.Server.Services.IServices;
using TTT.PersonalTool.Shared.IRepositories;

namespace TTT.PersonalTool.Server;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddPersonalTool(this IServiceCollection services)
    {
        services.AddSingleton<ILoggerProvider, ApplicationLoggerProvider>();
        services.AddScoped<IControlDataProvider, ControlDataByTenant>();
        services.AddScoped<ITTTSercurity, TTTSercurity>();
        services.AddScoped<ICoreSystermTTT, CoreSystermTTT>();

        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<ITenantRepository, TenantRepository>();

        return services;
    }
}
