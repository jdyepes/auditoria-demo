using Auditing.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace Auditing.Web.Pages.ViewModels
{
    public class AuditVm
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El título es obligatorio.")]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "El área auditada es obligatoria.")]
        public string AuditedArea { get; set; } = string.Empty;

        [Required(ErrorMessage = "La fecha de inicio es obligatoria.")]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "La fecha de fin es obligatoria.")]
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }

        [Required(ErrorMessage = "Debe seleccionar un responsable.")]
        public int OwnerId { get; set; }

        public AuditStatus Status { get; set; }

    }
}
