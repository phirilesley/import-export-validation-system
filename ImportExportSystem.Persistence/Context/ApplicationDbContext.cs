using Microsoft.EntityFrameworkCore;
using ImportExportSystem.Domain.Entities;

namespace ImportExportSystem.Persistence.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<ImportJob> ImportJobs { get; set; }
        public DbSet<ImportJobRow> ImportJobRows { get; set; }
        public DbSet<ImportError> ImportErrors { get; set; }
        public DbSet<ExportJob> ExportJobs { get; set; }
        public DbSet<UploadedFile> UploadedFiles { get; set; }
        public DbSet<AuditLog> AuditLogs { get; set; }
        public DbSet<ImportRowStatus> ImportRowStatuses { get; set; }
        public DbSet<ImportRetryLog> ImportRetryLogs { get; set; }
        public DbSet<ColumnMapping> ColumnMappings { get; set; }
        public DbSet<ValidationRule> ValidationRules { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configurations can be added here or in separate files
        }
    }
}