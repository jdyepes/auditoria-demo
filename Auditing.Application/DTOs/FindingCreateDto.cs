using Auditing.Domain.Enums;

namespace Auditing.Application.DTOs
{
    public record FindingCreateDto(
        int AuditId,
        string Description,
        FindingType Type,
        SeverityLevel Severity,
        DateTime DetectionDate
    );
}