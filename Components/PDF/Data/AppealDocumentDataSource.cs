using SAPA.Components.PDF.Model;

namespace SAPA.Components.PDF.Data{
    public static class AppealDocumentDataSource
    {
        public static AppealModel GetSampleAppeal()
        {
            return new AppealModel
            {
            StudentName = "John Doe", // Replace with dynamic data
            StudentID = "123456789", // Replace with dynamic data
            AppealReason = "Request for re-evaluation of grade.", // Replace with dynamic input
            RequestedAction = "Regrade final project.", // Replace with dynamic input
            Comments = "I believe the grade does not reflect the actual effort and quality of my work." // Replace with user input
            };
        }
    }
}