using System.ComponentModel.DataAnnotations;

namespace BlazorApp;

public class AppealFormModel1
{
    [Required]
    public string Name { get; set; }
    [Required]
    public string StudentId { get; set; }
    [Required]
    public DateTime? Date { get; set; }
    [Required]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; }
    [Required]
    public string Major { get; set; }
    [Required]
    public string AppealExplanation { get; set; }
}