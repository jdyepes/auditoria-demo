using Auditing.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace Auditing.Web.Pages.ViewModels
{
    public class FindingVm
    {
        public int Id { get; set; }

        [Required]
        public int AuditId { get; set; }

        [Required]
        public string Description { get; set; } = default!;

        [Required]
        public FindingType Type { get; set; } = default!; // 0 = Observacion, 1 = No conforme

        [Required]
        [Range(0, 2)]
        public SeverityLevel Severity { get; set; } // 0 = Bajo, 1 = Medio, 2 = Alto

        [Required]
        public DateTime DetectionDate { get; set; }

        public DateTime? CloseDate { get; set; }

        public int? OwnerId { get; set; } // solo requerido si Severity = 2

        public bool IsClosed { get; set; }
    }
}
