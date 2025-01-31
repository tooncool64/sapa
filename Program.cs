using BlazorApp;
using BlazorApp.Components;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.FileProviders;
using Microsoft.Identity.Web;
using QuestPDF.Infrastructure;
using SAPA.Components.PDF.Templates;
using SAPA.Components.PDF.Data;
using QuestPDF.Fluent;
using Microsoft.Identity.Web.UI;

var builder = WebApplication.CreateBuilder(args);

//QuestPDF free license
QuestPDF.Settings.License = LicenseType.Community;

//QuestPDF compiler
var model = AppealDocumentDataSource.GetSampleAppeal(); // Get data
    var document = new AppealDocument(model);

   // document.GeneratePdf("StudentAppeal.pdf"); // Save as PDF

builder.Services.AddControllersWithViews();

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
        options.Instance = "https://login.microsoftonline.com/";
        options.TenantId = Environment.GetEnvironmentVariable("AzureAd__TenantID");
        options.ClientId = Environment.GetEnvironmentVariable("AzureAd__ClientID");
        options.CallbackPath = "/signin-oidc";
        options.SignedOutCallbackPath = "/signout-oidc";
        options.SignedOutRedirectUri = "https://sap-app-e2hbhkhuabe3hjd8.westus-01.azurewebsites.net/";
    });

builder.Services.AddAuthorizationBuilder()
    .AddPolicy("Admin", policy =>
        policy.RequireClaim("roles", "Admin"))
    .AddPolicy("Advisor", policy => 
        policy.RequireClaim("roles", "Advisor"));

builder.Services.Configure<CookieAuthenticationOptions>(CookieAuthenticationDefaults.AuthenticationScheme, 
    options => {
        options.SlidingExpiration = true;
        options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
    });

builder.Services.AddAntiforgery(options => options.HeaderName = "X-CSRF-TOKEN");

builder.Services.AddHttpContextAccessor();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseAntiforgery();

app.MapControllers();

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
        Path.Combine(".well-known")),
    RequestPath = "/.well-known"
});

app.UseStaticFiles();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();