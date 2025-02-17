namespace BlazorApp;

public class AppealService
{
    public static void SubmitAppeal(AppealFormModel1 appealForm1)
    {
        var appealForm = new AppealForm
        {
            Name = appealForm1.Name,
            StudentId = appealForm1.StudentId,
            Date = appealForm1.Date,
            Email = appealForm1.Email,
            Major = appealForm1.Major,
            AppealExplanation = appealForm1.AppealExplanation
        };
        
        
    }

    private class AppealForm
    {
        public string Name { get; set; }
        public string StudentId { get; set; }
        public DateTime? Date { get; set; }
        public string Email { get; set; }
        public string Major { get; set; }
    
        public string AppealExplanation { get; set; }
    }
}