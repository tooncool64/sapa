﻿using System.ComponentModel.DataAnnotations;

namespace BlazorApp;

public class AppealFormPart2
{
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

    [Required(ErrorMessage = "Advisor email is required")]
    [EmailAddress(ErrorMessage = "Enter a valid email address")]
    [StMartinEmail]
    public string AdvisorEmail { get; set; } = string.Empty;

    [Required(ErrorMessage = "At least one course is required per semester")]
    public List<SemesterCourseValid> Semester1Courses { get; set; } = new();

    [Required(ErrorMessage = "At least one course is required per semester")]
    public List<SemesterCourseValid> Semester2Courses { get; set; } = new();
        
    [Required(ErrorMessage = "Please choose the semester you will be taking these courses.")]
    public new SemesterType? Semester1Label { get; set; }
        
    [Required(ErrorMessage = "Please choose the semester you will be taking these courses.")]
    public new SemesterType? Semester2Label { get; set; }

}

public class SemesterCourseValid : CourseBase
{
    [Required(ErrorMessage = "Course Name is required")]
    public new string CourseName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Course Number is required")]
    public new string CourseNumber { get; set; } = string.Empty;

    [Required(ErrorMessage = "Credit Hours is required")]
    [Range(1, 6, ErrorMessage = "Credit Hours must be between 1 and 6")]
    public new int CreditHours { get; set; }
}

public class SemesterCourse : CourseBase
{ }

public enum SemesterType
{
    Summer,
    Fall,
    Spring
}