using Auditing.Domain.Enums;

namespace Auditing.Domain.Entities
{
    // Hallazgo dentro del agregado Audit (reglas de eliminación se aplican en aplicación)
    public class Finding
    {
        public int Id { get; private set; }                           // Id hallazgo
        public int AuditId { get; private set; }                      // FK auditoría
        public Audit Audit { get; private set; } = default!;          // Navegación
        public string Description { get; private set; } = default!;   // Descripción
        public FindingType Type { get; private set; }                 // Tipo
        public SeverityLevel Severity { get; private set; }           // Severidad
        public DateTime DetectionDate { get; private set; }           // Fecha detección
        public DateTime CreatedAtUtc { get; private set; } = DateTime.UtcNow; // Creación

        public Finding() { }

        public Finding(int auditId, SeverityLevel severity, string description)
        {
            AuditId = auditId;
            Severity = severity;
            Description = description;
        }
        public void ChangeDescription(string newDescription) => Description = newDescription;
        public void ChangeSeverity(SeverityLevel newSeverity) => Severity = newSeverity;

        // Fábrica con validaciones
        public static Finding Create(int auditId, string description, FindingType type, SeverityLevel severity, DateTime detectionDate)
        {
            if (auditId <= 0) throw new ArgumentException("AuditId must be a positive value");
            if (string.IsNullOrWhiteSpace(description)) throw new ArgumentException("Description is required");

            var finding = new Finding(auditId, severity, description.Trim());
            finding.Type = type;
            finding.DetectionDate = detectionDate.Date;

            return finding;

        }

        public void Update(string description, FindingType type, SeverityLevel severity, DateTime detectionDate)
        {
            Description = description.Trim();
            Type = type;
            Severity = severity;
            DetectionDate = detectionDate.Date;
        }

    }
}