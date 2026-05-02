namespace ImportExportSystem.Domain.Entities
{
    public class ImportJob
    {
        public Guid Id { get; set; }
        public string FileName { get; set; }
        public string Status { get; set; }
        public int TotalRows { get; set; }
        public int SuccessfulRows { get; set; }
        public int FailedRows { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}