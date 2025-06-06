﻿using Newtonsoft.Json;

namespace BlazorApp;

public class AppealForm
{
    [JsonProperty(PropertyName = "id")]
    public string Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string StudentId { get; set; } = string.Empty;
    public DateTime? Date { get; set; } = DateTime.Now;
    public string Email { get; set; } = string.Empty;
    public string Major { get; set; } = string.Empty;
    public string AppealExplanation { get; set; } = string.Empty;
    public string DegreeProgram { get; set; } = string.Empty;
    public DateTime? GradDate { get; set; }
    public decimal GPA { get; set; }
    public int DegreeHours { get; set; }
    public bool ChangeMajor { get; set; } = false;
    public List<SemesterCourse> Semester1Courses { get; set; } = new();
    public List<SemesterCourse> Semester2Courses { get; set; } = new();
    public bool AcknowledgementPayment { get; set; }
    public bool AcknowledgementFinal { get; set; }
    // open or close
    public string Status { get; set; } = "Pending"; // default for students
    
    public virtual List<AppealComment> Comments { get; set; } = new List<AppealComment>();
    public string AdvisorEmail { get; set; } = string.Empty;
    
    public string AdvisorApprovalStatus { get; set; } = "Pending";
    
    public List<UploadedFileInfo> UploadedFiles { get; set; } = new List<UploadedFileInfo>();
    
    public new SemesterType? Semester1Label { get; set; }

    public new SemesterType? Semester2Label { get; set; }

}

public class CourseBase
{
    [JsonProperty(PropertyName = "id")]
    public string Id { get; set; }
    public string CourseName { get; set; } = string.Empty;
    public string CourseNumber { get; set; } = string.Empty;
    public int CreditHours { get; set; }
    public bool IsRepeat { get; set; } = false;
    public bool IsRequiredForMajor { get; set; } = false;
}

public class UploadedFileInfo
{
    public string FileId { get; set; } = string.Empty;
    public string OriginalFileName { get; set; } = string.Empty;
}