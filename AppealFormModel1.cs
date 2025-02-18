using System.ComponentModel.DataAnnotations;

namespace BlazorApp;

public class AppealFormModel1
{
    [Required(ErrorMessage = "Name is required")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "Student ID is required")]
    public string StudentId { get; set; } = string.Empty;

    [Required(ErrorMessage = "Date is required")]
    public DateTime? Date { get; set; } = DateTime.Today;

    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Invalid email address")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Major is required")]
    public string Major { get; set; } = string.Empty;

    [Required(ErrorMessage = "Appeal explanation is required")]
    public string AppealExplanation { get; set; } = string.Empty;
}