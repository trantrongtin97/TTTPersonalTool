using TTT.PersonalTool.Contracts.IRepositories;
using TTT.PersonalTool.Server.Logging;
using TTT.PersonalTool.Server.Repositories;
using TTT.PersonalTool.Server.Sercurity;
using TTT.PersonalTool.Server.Services.IServices;

namespace TTT.PersonalTool.Server;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddPersonalTool(this IServiceCollection services)
    {
        services.AddSingleton<ILoggerProvider, ApplicationLoggerProvider>();
        services.AddScoped<ITTTSercurity, TTTSercurity>();

        services.AddScoped<IUserRepository, UserRepository>();

        return services;
    }
}
