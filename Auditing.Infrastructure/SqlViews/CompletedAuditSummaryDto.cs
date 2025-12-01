namespace Auditing.Infrastructure.SqlViews
{
    // DTO para mapear la vista dbo.vCompletedAuditsSummary
    public class CompletedAuditSummaryDto
    {
        public int Id { get; set; }                 // Id auditoría
        public string Title { get; set; } = default!;
        public string AuditedArea { get; set; } = default!;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string OwnerName { get; set; } = default!;
        public string OwnerArea { get; set; } = default!;
        public int FindingsLow { get; set; }
        public int FindingsMedium { get; set; }
        public int FindingsHigh { get; set; }
    }
}