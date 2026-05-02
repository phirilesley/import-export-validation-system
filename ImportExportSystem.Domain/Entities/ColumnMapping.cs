namespace ImportExportSystem.Domain.Entities
{
    public class ColumnMapping
    {
        public Guid Id { get; set; }
        public string SourceColumn { get; set; }
        public string TargetField { get; set; }
        public Guid ImportJobId { get; set; }
    }
}