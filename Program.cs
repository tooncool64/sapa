using BlazorApp;
using BlazorApp.Components;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.Identity.Web;
using QuestPDF.Infrastructure;
using SAPA.Components.PDF.Templates;
using SAPA.Components.PDF.Data;
using QuestPDF.Fluent;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;

var builder = WebApplication.CreateBuilder(args);

//QuestPDF free license
QuestPDF.Settings.License = LicenseType.Community;

//QuestPDF compiler
var model = AppealDocumentDataSource.GetSampleAppeal(); // Get sample data
    var document = new AppealDocument(model);

    document.GeneratePdf("StudentAppeal.pdf"); // Save as PDF

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddDbContext<CosmosContext>(options =>
{
    var endpointUri = Environment.GetEnvironmentVariable("DATABASE_URI");
    var primaryKey = Environment.GetEnvironmentVariable("DATABASE_PRIMARY_KEY");
    options.UseCosmos(endpointUri, primaryKey, databaseName: "Users");
});

builder.Services.AddMsalAuthentication(options =>
{
    builder.Configuration.Bind("AzureAd", options.ProviderOptions.Authentication);


    options.ProviderOptions.DefaultAccessTokenScopes.Add("api://your-api-client-id/.default");

   
    options.ProviderOptions.LoginMode = "redirect"; // Options: "popup" or "redirect"
});


builder.Services.AddAuthorizationBuilder()
    .AddPolicy("Admin", policy => policy.RequireRole("Admin"))
    .AddPolicy("Advisor", policy => policy.RequireRole("Advisor"));


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