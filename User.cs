using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace BlazorApp;

public class User
{
    public int Id { get; set; }
    
    [Required]
    public string Username;
    
    [Required]
    public string Password;
}