using Auditing.Domain.Entities;

namespace Auditing.Domain.Repositories
{
    // Contrato de persistencia para auditorías
    public interface IAuditRepository
    {
        Task<Audit?> GetByIdAsync(int id);
        Task AddAsync(Audit audit);
        Task UpdateAsync(Audit audit);
        Task<List<Audit>> GetByDateRangeAndStatusAsync(DateTime start, DateTime end, int? status);
        Task<List<Audit>> GetByOwnerAsync(int ownerId);
    }
}