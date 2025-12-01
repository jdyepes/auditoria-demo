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

            var status = dto.Status == 0 ? AuditStatus.Pending : (AuditStatus)dto.Status;

            var audit = Audit.Create(dto.Title, dto.StartDate, dto.EndDate, dto.AuditedArea, dto.OwnerId, status);
            await _audits.AddAsync(audit);
            return audit;
        }

        public async Task<Audit> UpdateAsync(int id, AuditUpdateDto dto)
        {
            var audit = await _audits.GetByIdAsync(id)
                ?? throw new KeyNotFoundException("Audit not found");

            audit.Update(dto.Title, dto.StartDate, dto.EndDate, dto.AuditedArea, dto.OwnerId);
            await _audits.UpdateAsync(audit);
            return audit;
        }

        public async Task<Audit> ChangeStatusAsync(int id, AuditStatus newStatus)
        {
            var audit = await _audits.GetByIdAsync(id)
                ?? throw new KeyNotFoundException("Audit not found");

            // Si el newStatus es igual al actual, lo interpretamos como instrucción para avanzar
            if (newStatus == audit.Status)
            {
                newStatus = audit.Status switch
                {
                    AuditStatus.Pending => AuditStatus.InProgress,
                    AuditStatus.InProgress => AuditStatus.Completed,
                    AuditStatus.Completed => throw new InvalidOperationException("La auditoría ya está finalizada."),
                    _ => throw new ArgumentOutOfRangeException("Estado desconocido.")
                };
            }

            //  Validación final
            if (newStatus < audit.Status)
                throw new InvalidOperationException("No se puede revertir el estado.");

            if (newStatus > AuditStatus.Completed)
                throw new ArgumentOutOfRangeException("Estado inválido.");

            audit.ChangeStatus(newStatus);
            await _audits.UpdateAsync(audit);
            return audit;
        }

        public async Task<List<Audit>> GetAllAsync() =>
             await _audits.GetAllAsync();

        public async Task<Audit?> GetByIdAsync(int id) =>
               await _audits.GetByIdAsync(id);

        public Task<List<Audit>> GetByDateRangeAndStatusAsync(AuditQueryDto q) =>
            _audits.GetByDateRangeAndStatusAsync(q.StartDate.Date, q.EndDate.Date, q.Status);

        public Task<List<Audit>> GetByOwnerAsync(int ownerId) =>
            _audits.GetByOwnerAsync(ownerId);
    }
}