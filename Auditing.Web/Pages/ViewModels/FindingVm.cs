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
        public string Type { get; set; } = default!;

        [Required]
        [Range(0, 2)]
        public int Severity { get; set; } // 0 = Bajo, 1 = Medio, 2 = Alto

        [Required]
        public DateTime DetectionDate { get; set; }

        public DateTime? CloseDate { get; set; }

        public int? OwnerId { get; set; } // solo requerido si Severity = 2

        public bool IsClosed { get; set; }
    }
}
