using System.ComponentModel.DataAnnotations;

namespace BlazorApp;

public class AppealFormPart1
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Name is required")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "Student ID is required")]
    public string StudentId { get; set; } = string.Empty;

    [Required(ErrorMessage = "Date is required")]
    public DateTime? Date { get; set; } = DateTime.Now;

    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Please enter a valid email address")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Major is required")]
    public string Major { get; set; } = string.Empty;

    [Required(ErrorMessage = "Appeal explanation is required")]
    public string AppealExplanation { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "You must acknowledge this statement")]
    [Range(typeof(bool), "true", "true", ErrorMessage = "You must acknowledge this statement")]
    public bool AcknowledgementPayment { get; set; }
    [Required(ErrorMessage = "You must acknowledge this statement")]
    [Range(typeof(bool), "true", "true", ErrorMessage = "You must acknowledge this statement")]
    public bool AcknowledgementFinal { get; set; }
}