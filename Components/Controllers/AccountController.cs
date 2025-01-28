using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Mvc;

namespace BlazorApp.Components.Controllers;

[Microsoft.AspNetCore.Components.Route("logout")]
public class AccountController : Controller
{
    [HttpGet]
    public IActionResult Logout()
    {
        // Clear local cookies
        return SignOut(
            new AuthenticationProperties
            {
                RedirectUri = "/" // Redirect here after logout
            },
            OpenIdConnectDefaults.AuthenticationScheme,
            CookieAuthenticationDefaults.AuthenticationScheme
        );
    }
}