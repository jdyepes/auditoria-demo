using Auditing.Application.DTOs;
using Auditing.Domain.Entities;
using Auditing.Domain.Repositories;

namespace Auditing.Application.Services
{
    public class OwnerAppService
    {
        private readonly IOwnerRepository _owners;
        public OwnerAppService(IOwnerRepository owners) => _owners = owners;

        public async Task<Owner> CreateAsync(OwnerCreateDto dto)
        {
            var owner = Owner.Create(dto.Name, dto.Email, dto.Area);
            await _owners.AddAsync(owner);
            return owner;
        }

        public async Task<Owner> UpdateAsync(int id, OwnerUpdateDto dto)
        {
            var owner = await _owners.GetByIdAsync(id)
                ?? throw new KeyNotFoundException("Owner not found");

            owner.Update(dto.Name, dto.Email, dto.Area);
            await _owners.UpdateAsync(owner);
            return owner;
        }

        public Task<List<Owner>> GetAllAsync() => _owners.GetAllAsync();
        public Task<Owner?> GetByIdAsync(int id) => _owners.GetByIdAsync(id);
    }
}