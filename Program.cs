using BlazorApp;
using BlazorApp.Components;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
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
        options.Events = new OpenIdConnectEvents 
        {
            OnRedirectToIdentityProviderForSignOut = context => 
            {
                var postLogoutRedirectUri = builder.Configuration["AzureAd:PostLogoutRedirectUri"];
                context.ProtocolMessage.PostLogoutRedirectUri = postLogoutRedirectUri;
                return Task.CompletedTask;
            }
        };
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
        options.LogoutPath = "/logout";
    });

builder.Services.AddAntiforgery(options => options.HeaderName = "X-CSRF-TOKEN");

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
app.UseRouting();
app.UseAuthorization();
app.UseAntiforgery();

app.UseStaticFiles();


app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();