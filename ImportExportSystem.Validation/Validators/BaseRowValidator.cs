using System.Collections.Generic;

namespace ImportExportSystem.Validation.Validators
{
    public abstract class BaseRowValidator
    {
        public abstract ValidationResult Validate(Dictionary<string, string> row);
    }
}