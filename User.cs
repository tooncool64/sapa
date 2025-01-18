using System.ComponentModel.DataAnnotations;
namespace BlazorApp;

public class User
{
    [Required]
    public string Username;
    
    [Required]
    public string Password;
}