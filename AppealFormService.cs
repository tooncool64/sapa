using Microsoft.EntityFrameworkCore;

namespace BlazorApp;

using System.ComponentModel.DataAnnotations;

public interface IAppealFormService
{
    AppealForm CurrentForm { get; }
    bool UpdateForm(Action<AppealForm> updateAction);
    Task<bool> SubmitFormAsync();
    void ResetForm();
    event Action OnFormChanged;
    Task<bool> UpdateStatusAsync(string id, string newStatus);
    string GetLastErrorMessage();
}

public class AppealFormService : IAppealFormService
{
    private readonly AppealForm _form = new AppealForm();
    private readonly CosmosContext _dbContext;
    private string _lastErrorMessage;
    
    public event Action OnFormChanged;
    
    public AppealFormService(CosmosContext dbContext)
    {
        _dbContext = dbContext;
        // Initialize default values
        _form.Date = DateTime.Today;
    }
    
    public string GetLastErrorMessage()
    {
        return _lastErrorMessage;
    }
    
    public AppealForm CurrentForm => _form;
    
    public bool UpdateForm(Action<AppealForm> updateAction)
    {
        // Create a temporary copy to validate
        var tempForm = CloneForm(_form);
        
        // Apply the update to the temp form
        updateAction(tempForm);
        
        // Validate the form after update
        if (!ValidateForm(tempForm))
        {
            return false;
        }
        
        // If valid, apply to the real form
        updateAction(_form);
        
        // Notify listeners that the form has changed
        OnFormChanged?.Invoke();
        
        return true;
    }
    
    private bool ValidateForm(AppealForm form)
    {
        var validationContext = new ValidationContext(form);
        var validationResults = new List<ValidationResult>();
        
        return Validator.TryValidateObject(form, validationContext, validationResults, true);
    }
    
    private AppealForm CloneForm(AppealForm original)
    {
        // Create a simple clone for validation
        var clone = new AppealForm
        {
            Id = original.Id,
            Name = original.Name,
            StudentId = original.StudentId,
            Date = original.Date,
            Email = original.Email,
            Major = original.Major,
            AppealExplanation = original.AppealExplanation,
            DegreeProgram = original.DegreeProgram,
            GradDate = original.GradDate,
            GPA = original.GPA,
            DegreeHours = original.DegreeHours,
            ChangeMajor = original.ChangeMajor,
            AcknowledgementPayment = original.AcknowledgementPayment,
            AcknowledgementFinal = original.AcknowledgementFinal,
            Status = original.Status
        };
    
        // Clone Semester1Courses collection
        if (original.Semester1Courses != null)
        {
            clone.Semester1Courses = original.Semester1Courses.Select(c => new SemesterCourse
            {
                Id = c.Id,
                CourseName = c.CourseName,
                CourseNumber = c.CourseNumber,
                CreditHours = c.CreditHours,
                IsRepeat = c.IsRepeat,
                IsRequiredForMajor = c.IsRequiredForMajor
                // Clone any additional properties
            }).ToList();
        }
    
        // Clone Semester2Courses collection
        if (original.Semester2Courses != null)
        {
            clone.Semester2Courses = original.Semester2Courses.Select(c => new SemesterCourse
            {
                Id = c.Id,
                CourseName = c.CourseName,
                CourseNumber = c.CourseNumber,
                CreditHours = c.CreditHours,
                IsRepeat = c.IsRepeat,
                IsRequiredForMajor = c.IsRequiredForMajor
                // Clone any additional properties
            }).ToList();
        }
    
        return clone;
    }
// Keep the original method signature to match the interface
    public async Task<bool> SubmitFormAsync()
    {
        try
        {
            // Clear any previous error message
            _lastErrorMessage = null;
        
            // Validate before submission
            if (!ValidateForm(_form))
            {
                _lastErrorMessage = "Form validation failed. Please ensure all required fields are completed correctly.";
                return false;
            }
        
            // default for status
            if (string.IsNullOrWhiteSpace(_form.Status))
            {
                _form.Status = "Pending";
            }
            
            _dbContext.Appeals.Add(_form);
     
        
            // Save to the database
            await _dbContext.SaveChangesAsync();
            return true;
        }
        catch (DbUpdateException dbEx)
        {
            _lastErrorMessage = $"A database error occurred while saving your appeal:{dbEx.Message} {dbEx.InnerException!.Message} Please try again later.";
            return false;
        }
        catch (Exception ex) 
        {
            _lastErrorMessage = $"An error occurred while saving your appeal:{ex.Message} Please try again later.";
            return false;
        }
    }
    
    public void ResetForm()
    {
        // Explicitly reset each property to avoid reflection issues
        _form.Name = string.Empty;
        _form.StudentId = string.Empty;
        _form.Date = DateTime.Today;
        _form.Email = string.Empty;
        _form.Major = string.Empty;
        _form.AppealExplanation = string.Empty;
        _form.DegreeProgram = string.Empty;
        _form.GradDate = null;
        _form.GPA = 0;
        _form.DegreeHours = 0;
        _form.ChangeMajor = false;
        _form.AcknowledgementPayment = false;
        _form.AcknowledgementFinal = false;
    
        // Clear course collections
        _form.Semester1Courses.Clear();
        _form.Semester2Courses.Clear();
    
        // Notify listeners that the form has changed
        OnFormChanged?.Invoke();
    }
    public async Task<bool> UpdateStatusAsync(string id, string newStatus)
    {
        var appeal = await _dbContext.Appeals.FindAsync(id);
        if (appeal == null) return false;

        appeal.Status = newStatus;
        await _dbContext.SaveChangesAsync();
        return true;
    }
}