using System.ComponentModel.DataAnnotations;

public class HomePageOptions
{
    // Header Section
    [Required(ErrorMessage = "Page title is required")]
    [StringLength(100, ErrorMessage = "Title cannot exceed 100 characters")]
    public string Title { get; set; } = string.Empty;

    [Required(ErrorMessage = "Introduction text is required")]
    public string IntroText1 { get; set; } = string.Empty;

    [Required(ErrorMessage = "Introduction text is required")]
    public string IntroText2 { get; set; } = string.Empty;

    // Eligibility Section
    [Required(ErrorMessage = "Eligibility title is required")]
    public string EligibilityTitle { get; set; } = string.Empty;

    [Required(ErrorMessage = "Eligibility text is required")]
    public string EligibilityText { get; set; } = string.Empty;

    // SAP Status Section
    [Required(ErrorMessage = "SAP status title is required")]
    public string SapStatusTitle { get; set; } = string.Empty;

    [Required(ErrorMessage = "Warning title is required")]
    public string WarningTitle { get; set; } = string.Empty;

    [Required(ErrorMessage = "Warning text is required")]
    public string WarningText { get; set; } = string.Empty;

    [Required(ErrorMessage = "Must appeal title is required")]
    public string MustAppealTitle { get; set; } = string.Empty;

    [Required(ErrorMessage = "Must appeal text is required")]
    public string MustAppealText { get; set; } = string.Empty;

    public string MustAppealExtra { get; set; } = string.Empty;

    // Appeal Process Section
    [Required(ErrorMessage = "Appeal process title is required")]
    public string AppealProcessTitle { get; set; } = string.Empty;

    [Required(ErrorMessage = "Appeal process text is required")]
    public string AppealText { get; set; } = string.Empty;

    [Required(ErrorMessage = "Approved appeals title is required")]
    public string ApprovedAppealsTitle { get; set; } = string.Empty;

    [Required(ErrorMessage = "Approved appeals text is required")]
    public string ApprovedAppealsText { get; set; } = string.Empty;

    [Required(ErrorMessage = "Denied appeals title is required")]
    public string DeniedAppealsTitle { get; set; } = string.Empty;

    [Required(ErrorMessage = "Denied appeals text is required")]
    public string DeniedAppealsText { get; set; } = string.Empty;

    public string DeniedExtra { get; set; } = string.Empty;

    // Final Note
    [Required(ErrorMessage = "Final note is required")]
    public string FinalNote { get; set; } = string.Empty;
}