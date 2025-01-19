using BlazorApp;
using BlazorApp.Components;
using Microsoft.EntityFrameworkCore;

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

builder.Services.AddOidcAuthentication(options =>
{
    builder.Configuration.Bind("AzureAd", options.ProviderOptions);

    options.ProviderOptions.Authority = Environment.GetEnvironmentVariable("AzureAd__Authority");
    options.ProviderOptions.ClientId = Environment.GetEnvironmentVariable("AzureAd__ClientId");
    options.ProviderOptions.RedirectUri = Environment.GetEnvironmentVariable("AzureAd_RedirectUri");
});

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