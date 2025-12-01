using Auditing.Domain.Entities;

namespace Auditing.Domain.Repositories
{
    // Contrato de persistencia para hallazgos
    public interface IFindingRepository
    {
        Task<Finding?> GetByIdAsync(int id);
        Task AddAsync(Finding finding);
        Task DeleteAsync(Finding finding);
        Task<List<Finding>> GetByAuditAndSeverityAsync(int auditId, int severity);
    }
}