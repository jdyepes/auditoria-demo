namespace Auditing.Application.DTOs
{
    public record AuditQueryDto(
        DateTime StartDate,
        DateTime EndDate,
        int Status);
}