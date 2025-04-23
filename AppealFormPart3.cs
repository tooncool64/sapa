using System.ComponentModel.DataAnnotations;

namespace BlazorApp;

public class AppealFormPart3
{

    [Required(ErrorMessage = "Appeal explanation is required")]
    public string AppealExplanation { get; set; } = string.Empty;

}