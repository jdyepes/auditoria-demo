using Auditing.Application.DTOs;
using Auditing.Domain.Entities;
using Auditing.Domain.Enums;
using Auditing.Domain.Repositories;

namespace Auditing.Application.Services
{
    public class FindingAppService
    {
        private readonly IFindingRepository _findings;
        private readonly IAuditRepository _audits;
    

        public FindingAppService(IFindingRepository findings, IAuditRepository audits)
        {
            _findings = findings;
            _audits = audits;
        }

        public async Task<Finding> CreateAsync(FindingCreateDto dto)
        {
            var audit = await _audits.GetByIdAsync(dto.AuditId)
           ?? throw new KeyNotFoundException("Audit not found");          

            var finding = Finding.Create(dto.AuditId, dto.Description, dto.Type, dto.Severity, dto.DetectionDate);
            await _findings.AddAsync(finding);
            return finding;
        }

        public async Task<Finding> UpdateAsync(int id, FindingUpdateDto dto)
        {
            var finding = await _findings.GetByIdAsync(id)
                ?? throw new KeyNotFoundException("Finding not found");

            finding.Update(dto.Description, dto.Type, dto.Severity, dto.DetectionDate);
            await _findings.AddAsync(finding); 
            return finding;
        }

        public Task<List<Finding>> GetByAuditAndSeverityAsync(int auditId, int severity) =>
              _findings.GetByAuditAndSeverityAsync(auditId, severity);

        public Task<Finding?> GetByIdAsync(int id) => _findings.GetByIdAsync(id);

        public async Task DeleteAsync(int id)
        {
            var finding = await _findings.GetByIdAsync(id)
                ?? throw new KeyNotFoundException("Finding not found");

            var audit = await _audits.GetByIdAsync(finding.AuditId)
                ?? throw new KeyNotFoundException("Audit not found");

            if (audit.Status != AuditStatus.InProgress)
                throw new InvalidOperationException("Only findings from audits in progress can be deleted");

            await _findings.DeleteAsync(finding);
        }

    }
}