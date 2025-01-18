using System.ComponentModel.DataAnnotations;
namespace BlazorApp;

public class User
{
    public int UserId { get; set; }
    
    [Required]
    public string Username;
    
    [Required]
    public string Password;
}