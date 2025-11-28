namespace Auditing.Application.DTOs
{
    public record AuditCreateDto(
        string Title,
        DateTime StartDate,
        DateTime EndDate,
        string AuditedArea,
        int OwnerId);
}