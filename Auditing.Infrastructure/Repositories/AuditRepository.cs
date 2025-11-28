using Auditing.Domain.Entities;
using Auditing.Domain.Repositories;
using Auditing.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Auditing.Infrastructure.Repositories
{
    // Implementación EF Core del repositorio de auditorías
    public class AuditRepository : IAuditRepository
    {
        private readonly AuditingDbContext _db;
        public AuditRepository(AuditingDbContext db) => _db = db;

        public Task<Audit?> GetByIdAsync(int id) =>
            _db.Audits
               .Include(a => a.Owner)
               .Include(a => a.Findings)
               .FirstOrDefaultAsync(a => a.Id == id);

        public async Task AddAsync(Audit audit)
        {
            _db.Audits.Add(audit);
            await _db.SaveChangesAsync();
        }

        public async Task UpdateAsync(Audit audit)
        {
            _db.Audits.Update(audit);
            await _db.SaveChangesAsync();
        }

        public Task<List<Audit>> GetByDateRangeAndStatusAsync(DateTime start, DateTime end, int status) =>
            _db.Audits
               .Include(a => a.Owner)
               .Where(a => a.StartDate >= start && a.EndDate <= end && (int)a.Status == status)
               .AsNoTracking()
               .ToListAsync();

        public Task<List<Audit>> GetByOwnerAsync(int ownerId) =>
            _db.Audits
               .Include(a => a.Owner)
               .Where(a => a.OwnerId == ownerId)
               .AsNoTracking()
               .ToListAsync();
    }
}