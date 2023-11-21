using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using TTT.PersonalTool.Client;
using TTT.PersonalTool.Client.Extensions;
using TTT.PersonalTool.Shared;

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


var host = builder.Build();
await host.SetDefaultCulture();
await host.RunAsync();

