using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Text;
using TTT.Framework.EfCore;
using TTT.Framework.Sercurity;
using TTT.PersonalTool.Server.Authorization;
using TTT.PersonalTool.Server.Logging;
using TTT.PersonalTool.Server.Services;
using TTT.PersonalTool.Server.Services.IServices;
using TTT.PersonalTool.Server.SwaggerUI;
using TTT.PersonalTool.Shared.Const;

namespace TTT.PersonalTool.Server;

public static class ServiceExtension
{
    public static IServiceCollection AddTTTCoreService(this IServiceCollection services)
    {
        services.AddSingleton<ILoggerProvider, ApplicationLoggerProvider>();
        services.AddScoped<IControlDataProvider, ControlDataByTenant>();
        services.AddScoped<ITTTSercurity, TTTSercurity>();
        services.AddScoped<ICoreSystermTTT, CoreSystermTTT>();
        return services;
    }

    public static IServiceCollection TTTRegisterRepository(this IServiceCollection services,string strNamespace)
    {
        var repositories = Assembly.GetExecutingAssembly().GetTypes()
             .Where(x => !x.IsAbstract && x.IsClass && typeof(ITTTRepository).IsAssignableFrom(x));
        foreach (var repos in repositories)
        {
            var a = repos.GetInterfaces().Where(t => t.Namespace == strNamespace).First();
            services.Add(new ServiceDescriptor(a, repos, ServiceLifetime.Scoped));
        }
        return services;
    }

    public static IServiceCollection TTTAddAutoMapper(this IServiceCollection services)
    {
        services.AddAutoMapper
            (typeof(PersonalToolProfile).Assembly);
        return services;
    }

    public static IServiceCollection TTTAuthorization(this IServiceCollection services)
    {
        services.AddScoped<IAuthorizationHandler, UserVersionHandler>();
        services.AddAuthorization(options =>
        {
            options.AddPolicy(nameof(TTTPermissions.Policy_LvFull), policy =>
            {
                policy.RequireRole(TTTPermissions.Policy_LvFull);
                policy.Requirements.Add(new VersionRequirement());
            });

            options.AddPolicy(nameof(TTTPermissions.Policy_LvEmployee), policy =>
            {
                policy.RequireRole(TTTPermissions.Policy_LvEmployee);
                policy.Requirements.Add(new VersionRequirement());
            });


            options.AddPolicy(nameof(TTTPermissions.Policy_LvManager), policy =>
            {
                policy.RequireRole(TTTPermissions.Policy_LvManager);
                policy.Requirements.Add(new VersionRequirement());
            });


            options.AddPolicy(nameof(TTTPermissions.Policy_LvAdmin), policy =>
            {
                policy.RequireRole(TTTPermissions.Policy_LvAdmin);
                policy.Requirements.Add(new VersionRequirement());
            });

        });
        return services;
    }

    public static IServiceCollection TTTAuthentication(this IServiceCollection services,string secretKey)
    {
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(jwtBearerOptions =>
        {
            jwtBearerOptions.RequireHttpsMetadata = true;
            jwtBearerOptions.SaveToken = true;
            jwtBearerOptions.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secretKey)),
                ValidateIssuer = false,
                ValidateAudience = false,
                ClockSkew = TimeSpan.Zero
            };
        });
        return services;
    }

    public static IServiceCollection AddTTTSwaggerServices(this IServiceCollection services)
    {
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "TTT.PersonalTool.WebAPI", Version = "v1" });
            c.DocumentFilter<SwaggerUIVisibilityFilter>();
        });
        return services;
    }

}
