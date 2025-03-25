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
}

public class AppealFormService : IAppealFormService
{
    private readonly AppealForm _form = new AppealForm();
    private readonly CosmosContext _dbContext;
    
    public event Action OnFormChanged;
    
    public AppealFormService(CosmosContext dbContext)
    {
        _dbContext = dbContext;
        // Initialize default values
        _form.Date = DateTime.Today;
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
        return new AppealForm
        {
            Name = original.Name,
            StudentId = original.StudentId,
            Date = original.Date,
            Email = original.Email,
            Major = original.Major,
            AppealExplanation = original.AppealExplanation,
            AcknowledgementPayment = original.AcknowledgementPayment,
            AcknowledgementFinal = original.AcknowledgementFinal,
            Status = original.Status //not changeable by students
        };
    }
    
    public async Task<bool> SubmitFormAsync()
    {
        try
        {
            // Validate before submission
            if (!ValidateForm(_form))
            {
                return false;
            }
            // default for status
            if (string.IsNullOrWhiteSpace(_form.Status))
            _form.Status = "Pending";
            
            // Check if this form is already being tracked
            if (string.IsNullOrWhiteSpace(_form.Id)) // Assuming Id is null or empty for new forms
            {
                _dbContext.Appeals.Add(_form);
            }
            else
            {
                _dbContext.Appeals.Update(_form);
            }
            
            // Save to the database
            await _dbContext.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error submitting form: {ex.Message}");
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
        _form.AcknowledgementPayment = false;
        _form.AcknowledgementFinal = false;
        
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