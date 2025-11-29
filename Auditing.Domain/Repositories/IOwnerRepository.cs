using Auditing.Domain.Entities;

namespace Auditing.Domain.Repositories
{
    // Contrato de persistencia para responsables
    public interface IOwnerRepository
    {  
        Task<Owner?> GetByIdAsync(int id);
        Task<List<Owner>> GetAllAsync();      
        Task<Owner?> GetByEmailAsync(string email);
        Task AddAsync(Owner owner);
        Task UpdateAsync(Owner owner);

    }
}