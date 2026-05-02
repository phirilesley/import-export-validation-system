namespace ImportExportSystem.Domain.Entities
{
    public class ImportRetryLog
    {
        public Guid Id { get; set; }
        public Guid ImportJobId { get; set; }
        public int RowNumber { get; set; }
        public string ErrorMessage { get; set; } = string.Empty;
        public DateTime RetriedAt { get; set; }
        public bool Success { get; set; }
    }
}
