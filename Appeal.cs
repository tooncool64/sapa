using System.ComponentModel.DataAnnotations;

namespace BlazorApp;

public class Appeal
{
    [Key]
    public System.Guid Id { get; set; }
    
    [Required]
    public string First_Name;
    
    [Required]
    public string Last_Name;

    [Required]
    public string AppealId;
    
    
    public DateTime DateTime = DateTime.Now;
}