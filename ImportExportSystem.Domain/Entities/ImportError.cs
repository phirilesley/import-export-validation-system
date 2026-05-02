namespace ImportExportSystem.Domain.Entities
{
    public class ImportError
    {
        public Guid Id { get; set; }
        public Guid ImportJobId { get; set; }
        public int RowNumber { get; set; }
        public string ErrorMessage { get; set; }
    }
}