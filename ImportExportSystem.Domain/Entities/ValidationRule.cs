namespace ImportExportSystem.Domain.Entities
{
    public class ValidationRule
    {
        public Guid Id { get; set; }
        public string FieldName { get; set; }
        public string RuleType { get; set; } // Required, Email, Numeric, etc.
        public string Parameters { get; set; } // JSON parameters
        public bool IsActive { get; set; }
    }
}