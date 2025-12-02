using Auditing.Domain.Enums;

namespace Auditing.Domain.Entities
{
    // Agregado raíz: auditoría (controla estado y actualizaciones válidas)
    public class Audit
    {
        public int Id { get; private set; }                         // Id
        public string Title { get; private set; } = default!;       // Título
        public DateTime StartDate { get; private set; }             // Fecha inicio
        public DateTime EndDate { get; private set; }               // Fecha fin
        public string AuditedArea { get; private set; } = default!; // Área auditada
        public int OwnerId { get; private set; }                    // FK responsable
        public Owner Owner { get; private set; } = default!;        // Navegación responsable
        public AuditStatus Status { get; private set; } = AuditStatus.Pending; // Estado
        public DateTime CreatedAtUtc { get; private set; } = DateTime.UtcNow;  // Creación
        public DateTime? UpdatedAtUtc { get; private set; }                     // Última actualización
        public ICollection<Finding> Findings { get; private set; } = new List<Finding>(); // Hallazgos

        private Audit() { }
        public Audit( int ownerId, string title, AuditStatus status, DateTime start, DateTime end)
        {
            OwnerId = ownerId;
            Title = title;
            Status = status;
            StartDate = start;
            EndDate = end;
        }

        public void ChangeTitle(string newTitle) => Title = newTitle;

        // Fábrica con invariantes
        public static Audit Create(string title, DateTime startDate, DateTime endDate, string auditedArea, int ownerId, AuditStatus status)
        {
            if (string.IsNullOrWhiteSpace(title)) throw new ArgumentException("Title is required");
            if (string.IsNullOrWhiteSpace(auditedArea)) throw new ArgumentException("AuditedArea is required");
            if (endDate.Date < startDate.Date) throw new ArgumentException("EndDate must be >= StartDate");
            if (ownerId <= 0) throw new ArgumentException("OwnerId must be a positive value");

            var audit = new Audit(ownerId, title.Trim(), status, startDate.Date, endDate.Date);
            audit.AuditedArea = auditedArea.Trim();
            return audit;

        }

        // Actualización permitida solo en estado Pending
        public void Update(string title, DateTime startDate, DateTime endDate, string auditedArea, int ownerId)
        {
            if (Status != AuditStatus.Pending) throw new InvalidOperationException("Only Pending audits can be updated");
            if (string.IsNullOrWhiteSpace(title)) throw new ArgumentException("Title is required");
            if (string.IsNullOrWhiteSpace(auditedArea)) throw new ArgumentException("AuditedArea is required");
            if (endDate.Date < startDate.Date) throw new ArgumentException("EndDate must be >= StartDate");
            if (ownerId <= 0) throw new ArgumentException("OwnerId must be a positive value");

            Title = title.Trim();
            StartDate = startDate.Date;
            EndDate = endDate.Date;
            AuditedArea = auditedArea.Trim();
            UpdatedAtUtc = DateTime.UtcNow;
            OwnerId = ownerId;
            UpdatedAtUtc = DateTime.UtcNow;
        }

        // Cambios de estado válidos: Pending→InProgress→Completed
        public void ChangeStatus(AuditStatus newStatus)
        {
            bool valid =
                (Status == AuditStatus.Pending && newStatus == AuditStatus.InProgress) ||
                (Status == AuditStatus.InProgress && newStatus == AuditStatus.Completed);

            if (!valid) throw new InvalidOperationException("Invalid status transition");
            Status = newStatus;
            UpdatedAtUtc = DateTime.UtcNow;
        }
    }
}
