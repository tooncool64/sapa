using System.ComponentModel.DataAnnotations;

namespace BlazorApp;

public class AppealForm
{
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
    [Required(ErrorMessage = "Degree Program is required")]
    public string DegreeProgram { get; set; } = string.Empty;

    [Required(ErrorMessage = "Graduation Date is required")]
    public DateTime? GradDate { get; set; }

    [Required(ErrorMessage = "GPA is required")]
    [Range(0, 4.0, ErrorMessage = "GPA must be between 0 and 4.0")]
    public decimal GPA { get; set; }

    [Required(ErrorMessage = "Cumulative Degree Hours is required")]
    [Range(0, 200, ErrorMessage = "Degree hours must be between 0 and 200")]
    public int DegreeHours { get; set; }

    public bool ChangeMajor { get; set; } = false;

    [Required(ErrorMessage = "At least one course is required per semester")]
    public List<SemesterCourse> Semester1Courses { get; set; } = new();

    [Required(ErrorMessage = "At least one course is required per semester")]
    public List<SemesterCourse> Semester2Courses { get; set; } = new();
    [Required(ErrorMessage = "Please check the checkbox before submitting.")]
    public bool AcknowledgementPayment { get; set; }
    [Required(ErrorMessage = "Please check the checkbox before submitting.")]
    public bool AcknowledgementFinal { get; set; }
}

public class SemesterCourse
{
    [Required(ErrorMessage = "Course Name is required")]
    public string CourseName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Course Number is required")]
    public string CourseNumber { get; set; } = string.Empty;

    [Required(ErrorMessage = "Credit Hours is required")]
    [Range(1, 6, ErrorMessage = "Credit Hours must be between 1 and 6")]
    public int CreditHours { get; set; }

    public bool IsRepeat { get; set; } = false;

    public bool IsRequiredForMajor { get; set; } = false;
}