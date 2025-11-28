using Auditing.Domain.Entities;

namespace Auditing.Domain.Repositories
{
    // Contrato de persistencia para responsables
    public interface IOwnerRepository
    {
        Task<Owner?> GetByIdAsync(int id);
        Task<Owner?> GetByEmailAsync(string email);
        Task AddAsync(Owner owner);
    }
}