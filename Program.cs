using BlazorApp;
using BlazorApp.Components;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.Identity.Web;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddDbContext<CosmosContext>(options =>
{
    var endpointUri = Environment.GetEnvironmentVariable("DATABASE_URI");
    var primaryKey = Environment.GetEnvironmentVariable("DATABASE_PRIMARY_KEY");
    options.UseCosmos(endpointUri, primaryKey, databaseName: "Users");
});

builder.Services.AddAuthentication(OpenIdConnectDefaults.AuthenticationScheme)
    .AddMicrosoftIdentityWebApp(options =>
    {
        builder.Configuration.Bind("AzureAd", options);

        // Optionally, override with environment variables
        options.Authority = Environment.GetEnvironmentVariable("AzureAd__Authority") ?? options.Authority;
        options.ClientId = Environment.GetEnvironmentVariable("AzureAd__ClientId") ?? options.ClientId;
        options.CallbackPath = Environment.GetEnvironmentVariable("AzureAd_RedirectUri") ?? options.CallbackPath;
    });

builder.Services.AddAuthorizationBuilder()
    .AddPolicy("Admin", policy => policy.RequireRole("Admin"));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();