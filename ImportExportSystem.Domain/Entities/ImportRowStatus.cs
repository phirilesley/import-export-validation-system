namespace ImportExportSystem.Domain.Entities
{
    public class ImportRowStatus
    {
        public Guid Id { get; set; }
        public Guid ImportJobId { get; set; }
        public int RowNumber { get; set; }
        public string Status { get; set; } // Pending, Success, Failed, Retried
        public DateTime LastUpdated { get; set; }
    }
}