namespace ImportExportSystem.Domain.Entities
{
    public class AuditLog
    {
        public Guid Id { get; set; }
        public string Action { get; set; }
        public string User { get; set; }
        public string Details { get; set; }
        public DateTime Timestamp { get; set; }
    }
}