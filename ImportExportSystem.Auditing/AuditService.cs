using ImportExportSystem.Domain.Entities;
using ImportExportSystem.Persistence.Context;
using System;
using System.Threading.Tasks;

namespace ImportExportSystem.Auditing
{
    public class AuditService
    {
        private readonly ApplicationDbContext _context;

        public AuditService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task LogActionAsync(string action, string user, string details)
        {
            var log = new AuditLog
            {
                Id = Guid.NewGuid(),
                Action = action,
                User = user,
                Details = details,
                Timestamp = DateTime.UtcNow
            };

            _context.AuditLogs.Add(log);
            await _context.SaveChangesAsync();
        }
    }
}