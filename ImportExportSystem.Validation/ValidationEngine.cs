using ImportExportSystem.Validation.Validators;
using System.Collections.Generic;

namespace ImportExportSystem.Validation
{
    public class ValidationEngine
    {
        private readonly BaseRowValidator _validator;

        public ValidationEngine(BaseRowValidator validator)
        {
            _validator = validator;
        }

        public ValidationResult ValidateRow(Dictionary<string, string> row)
        {
            return _validator.Validate(row);
        }
    }
}