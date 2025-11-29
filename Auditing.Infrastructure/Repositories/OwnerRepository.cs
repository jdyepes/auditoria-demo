using Auditing.Domain.Entities;
using Auditing.Domain.Repositories;
using Auditing.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Auditing.Infrastructure.Repositories
{
    public class OwnerRepository : IOwnerRepository
    {
        private readonly AuditingDbContext _db;
        public OwnerRepository(AuditingDbContext db) => _db = db;

        public Task<Owner?> GetByIdAsync(int id) =>
            _db.Owners.FirstOrDefaultAsync(o => o.Id == id);

        public Task<List<Owner>> GetAllAsync() =>
            _db.Owners.AsNoTracking().ToListAsync();

        public Task<Owner?> GetByEmailAsync(string email) =>
            _db.Owners.FirstOrDefaultAsync(a => a.Email == email);

        public async Task AddAsync(Owner owner)
        {
            _db.Owners.Add(owner);
            await _db.SaveChangesAsync();
        }

        public async Task UpdateAsync(Owner owner)
        {
            _db.Owners.Update(owner);
            await _db.SaveChangesAsync();
        }
    }
}