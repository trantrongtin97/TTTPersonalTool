using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using TTT.PersonalTool.Client;
using TTT.PersonalTool.Client.Extensions;
using TTT.PersonalTool.Shared;
using TTT.PersonalTool.Shared.Const;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

#region Configure App Client
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
#endregion

builder.Services.AddLocalization();

#region Add AppClient
var applicationSettingsSection = builder.Configuration.GetSection("ApplicationSettings");
builder.Services.Configure<ApplicationSettings>(options =>
{
    applicationSettingsSection.Bind(options);
});
builder.Services.AddPersonalTool(applicationSettingsSection.Get<ApplicationSettings>());
#endregion

builder.Services.AddAuthorizationCore(options =>
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

var host = builder.Build();
await host.SetDefaultCulture();
await host.RunAsync();

