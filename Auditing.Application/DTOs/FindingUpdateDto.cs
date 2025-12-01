using Auditing.Domain.Enums;

namespace Auditing.Application.DTOs
{
    public record FindingUpdateDto(
        string Description,
        FindingType Type,
        SeverityLevel Severity,
        DateTime DetectionDate,
        int? OwnerId,
        bool IsClosed,
        DateTime? CloseDate
    );
}