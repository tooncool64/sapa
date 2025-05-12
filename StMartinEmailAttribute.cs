using System.ComponentModel.DataAnnotations;

namespace BlazorApp
{
    public class StMartinEmailAttribute : ValidationAttribute
    {
        public StMartinEmailAttribute()
        {
            ErrorMessage = "Advisor email must be a @stmartin.edu address.";
        }

        public override bool IsValid(object? value)
        {
            var email = value as string;

            if (string.IsNullOrWhiteSpace(email))
                return false;

            return email.Trim().ToLower().EndsWith("@stmartin.edu");
        }
    }
}
