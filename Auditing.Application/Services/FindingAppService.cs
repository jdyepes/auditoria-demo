using Auditing.Application.DTOs;
using Auditing.Domain.Entities;
using Auditing.Domain.Enums;
using Auditing.Domain.Repositories;

namespace Auditing.Application.Services
{
    public class FindingAppService
    {
        private readonly IFindingRepository _repo;
        public FindingAppService(IFindingRepository repo) => _repo = repo;

        public async Task<Finding> CreateAsync(FindingCreateDto dto)
        {
            var finding = Finding.Create(dto.AuditId, dto.Description, dto.Type, dto.Severity, dto.DetectionDate);
            await _repo.AddAsync(finding);
            return finding;
        }

        public async Task<Finding> UpdateAsync(int id, FindingUpdateDto dto)
        {
            var finding = await _repo.GetByIdAsync(id)
                ?? throw new KeyNotFoundException("Finding not found");

            finding.Update(dto.Description, dto.Type, dto.Severity, dto.DetectionDate);
            await _repo.AddAsync(finding); // Reutilizamos AddAsync como upsert
            return finding;
        }

        public Task<List<Finding>> GetByAuditAndSeverityAsync(int auditId, int severity) =>
            _repo.GetByAuditAndSeverityAsync(auditId, severity);

        public Task<Finding?> GetByIdAsync(int id) => _repo.GetByIdAsync(id);

        public async Task DeleteAsync(int id)
        {
            var finding = await _repo.GetByIdAsync(id)
                ?? throw new KeyNotFoundException("Finding not found");

            await _repo.DeleteAsync(finding);
        }
    }
}