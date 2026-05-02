namespace ImportExportSystem.Domain.Entities
{
    public class ExportJob
    {
        public Guid Id { get; set; }
        public string FileName { get; set; }
        public string Status { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}