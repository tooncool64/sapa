using System.ComponentModel.DataAnnotations;

namespace BlazorApp;

public class User
{
    [Key]
    public System.Guid Id { get; set; }
    
    [Required]
    public string Username;
    
    [Required]
    public string Password;
}