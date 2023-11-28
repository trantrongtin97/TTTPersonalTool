using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using TTT.PersonalTool.Server;
using TTT.PersonalTool.Server.DbContexts;

var builder = WebApplication.CreateBuilder(args);
string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

#region ConfigureServices

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
            builder =>
            {
                builder.WithOrigins("https://localhost:44322")
                            .AllowAnyHeader()
                            .AllowAnyMethod();
            });
});

builder.Services.AddHttpContextAccessor();
builder.Services.AddControllers();

builder.Services.AddTTTSwaggerServices();

builder.Services.AddDbContext<DbPersonalToolContext>(options => options.UseSqlServer("Name=Default"));
builder.Services.AddDbContextFactory<DbLoggingContext>(options => options.UseSqlServer("Name=Default"));

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

builder.Services.TTTAuthentication(builder.Configuration["JWTSettings:SecretKey"]);

builder.Services.TTTAuthorization();

builder.Services.AddResponseCompression(opts =>
{
    opts.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(
        new[] { "application/octet-stream" });
});

builder.Services.AddHttpClient();

builder.Services.AddTTTCoreService();
builder.Services.TTTRegisterRepository("TTT.PersonalTool.Shared.IRepositories");
builder.Services.TTTAddAutoMapper();

builder.Services.AddRazorPages();
#endregion

var app = builder.Build();

#region ConfigureApp

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "TTT.PersonalTool.WebAPI v1"));
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseResponseCompression();
app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();
app.UseCors(MyAllowSpecificOrigins);

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();
app.MapControllers();

app.MapFallbackToFile("index.html");
#endregion

app.Run();
