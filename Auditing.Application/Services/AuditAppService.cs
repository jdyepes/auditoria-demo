using Auditing.Application.DTOs;
using Auditing.Domain.Entities;
using Auditing.Domain.Enums;
using Auditing.Domain.Repositories;

namespace Auditing.Application.Services
{
    public class AuditAppService
    {
        private readonly IAuditRepository _audits;
        private readonly IOwnerRepository _owners;

        public AuditAppService(IAuditRepository audits, IOwnerRepository owners)
        {
            _audits = audits;
            _owners = owners;
        }

        public async Task<Audit> CreateAsync(AuditCreateDto dto)
        {
            var owner = await _owners.GetByIdAsync(dto.OwnerId)
                ?? throw new ArgumentException("Owner not found");

            var audit = Audit.Create(dto.Title, dto.StartDate, dto.EndDate, dto.AuditedArea, dto.OwnerId);
            await _audits.AddAsync(audit);
            return audit;
        }

        public async Task<Audit> UpdateAsync(int id, AuditUpdateDto dto)
        {
            var audit = await _audits.GetByIdAsync(id)
                ?? throw new KeyNotFoundException("Audit not found");

            audit.Update(dto.Title, dto.StartDate, dto.EndDate, dto.AuditedArea);
            await _audits.UpdateAsync(audit);
            return audit;
        }

        public async Task<Audit> ChangeStatusAsync(int id, AuditStatus newStatus)
        {
            var audit = await _audits.GetByIdAsync(id)
                ?? throw new KeyNotFoundException("Audit not found");

            audit.ChangeStatus(newStatus);
            await _audits.UpdateAsync(audit);
            return audit;
        }

        public Task<List<Audit>> GetByDateRangeAndStatusAsync(AuditQueryDto q) =>
            _audits.GetByDateRangeAndStatusAsync(q.StartDate.Date, q.EndDate.Date, q.Status);

        public Task<List<Audit>> GetByOwnerAsync(int ownerId) =>
            _audits.GetByOwnerAsync(ownerId);
    }
}