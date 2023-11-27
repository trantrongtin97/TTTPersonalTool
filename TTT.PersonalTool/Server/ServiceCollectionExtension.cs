using Microsoft.AspNetCore.Rewrite;
using System.Data;
using System.Reflection;
using TTT.Framework.EfCore;
using TTT.Framework.Sercurity;
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

        var repositories = Assembly.GetExecutingAssembly().GetTypes()
             .Where(x => !x.IsAbstract && x.IsClass && typeof(IRepository).IsAssignableFrom(x));
        foreach (var repos in repositories)
        {
            var a = repos.GetInterfaces().Where(t=>t.Namespace == "TTT.PersonalTool.Shared.IRepositories").First();
            services.Add(new ServiceDescriptor(a, repos, ServiceLifetime.Scoped));
        }
        //services.AddScoped<IItemRepository, ItemRepository>();
        //services.AddScoped<IUserRepository, UserRepository>();
        //services.AddScoped<ITenantRepository, TenantRepository>();


        return services;
    }
}
