using Auditing.Domain.Entities;
using Auditing.Domain.Enums;
using Auditing.Domain.Repositories;
using Auditing.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Auditing.Infrastructure.Repositories
{
    public class FindingRepository : IFindingRepository
    {
        private readonly AuditingDbContext _db;
        public FindingRepository(AuditingDbContext db) => _db = db;

        public Task<Finding?> GetByIdAsync(int id) =>
            _db.Findings.FirstOrDefaultAsync(f => f.Id == id);

        public Task<List<Finding>> GetByAuditAndSeverityAsync(int auditId, int severity) =>
            _db.Findings
               .Where(f => f.AuditId == auditId && (int)f.Severity == severity)
               .AsNoTracking()
               .ToListAsync();

        public async Task AddAsync(Finding finding)
        {
            _db.Findings.Add(finding);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteAsync(Finding finding)
        {
            _db.Findings.Remove(finding);
            await _db.SaveChangesAsync();
        }
    }
}