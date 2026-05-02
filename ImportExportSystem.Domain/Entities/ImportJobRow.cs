namespace ImportExportSystem.Domain.Entities
{
    public class ImportJobRow
    {
        public Guid Id { get; set; }
        public Guid ImportJobId { get; set; }
        public int RowNumber { get; set; }
        public string Status { get; set; } // Pending, Success, Failed
        public string Data { get; set; } // JSON or something
        public string ErrorMessage { get; set; }
    }
}