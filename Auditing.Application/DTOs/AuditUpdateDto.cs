namespace Auditing.Application.DTOs
{
    public record AuditUpdateDto(
        string Title,
        DateTime StartDate,
        DateTime EndDate,
        string AuditedArea);
}