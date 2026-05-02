using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace ImportExportSystem.Validation.Validators
{
    public class StudentValidator : BaseRowValidator
    {
        public override ValidationResult Validate(Dictionary<string, string> row)
        {
            var result = new ValidationResult { IsValid = true };

            // Example validations
            if (!row.ContainsKey("Name") || string.IsNullOrWhiteSpace(row["Name"]))
            {
                result.IsValid = false;
                result.Errors.Add("Name is required.");
            }

            if (row.ContainsKey("Email") && !IsValidEmail(row["Email"]))
            {
                result.IsValid = false;
                result.Errors.Add("Invalid email format.");
            }

            if (row.ContainsKey("Age"))
            {
                var ageText = row["Age"];
                if (!int.TryParse(ageText, out var age) || age < 0 || age > 120)
                {
                    result.IsValid = false;
                    result.Errors.Add("Age must be a valid number between 0 and 120.");
                }
            }

            return result;
        }

        private bool IsValidEmail(string email)
        {
            var emailRegex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");
            return emailRegex.IsMatch(email);
        }
    }
}
