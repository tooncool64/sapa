namespace BlazorApp;

public interface IAppealFormService
{
    AppealForm CurrentForm { get; }
    void UpdateForm(Action<AppealForm> updateAction);
    Task<bool> SubmitFormAsync();
    void ResetForm();
}

public class AppealFormService : IAppealFormService
{
    private readonly AppealForm _form = new AppealForm();
    private readonly CosmosContext _dbContext;
    
    public AppealFormService(CosmosContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public AppealForm CurrentForm => _form;
    
    public void UpdateForm(Action<AppealForm> updateAction)
    {
        updateAction(_form);
    }
    
    public async Task<bool> SubmitFormAsync()
    {
        try
        {
            // Add the completed form to your DbContext
            _dbContext.Appeals.Add(_form);
            
            // Save to the database
            await _dbContext.SaveChangesAsync();
            return true;
        }
        catch
        {
            return false;
        }
    }
    
    public void ResetForm()
    {
        // Reset properties to default values
        var properties = typeof(AppealForm).GetProperties();
        foreach (var property in properties)
        {
            if (property.PropertyType == typeof(string))
                property.SetValue(_form, string.Empty);
            else if (property.PropertyType == typeof(bool))
                property.SetValue(_form, false);
            else if (property.PropertyType == typeof(DateTime))
                property.SetValue(_form, DateTime.Now);
            else if (property.PropertyType.IsGenericType && 
                     property.PropertyType.GetGenericTypeDefinition() == typeof(List<>))
            {
                var list = Activator.CreateInstance(property.PropertyType);
                property.SetValue(_form, list);
            }
        }
    }
}