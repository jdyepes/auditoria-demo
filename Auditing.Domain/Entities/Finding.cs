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

        // Fábrica con validaciones
        public static Finding Create(int auditId, string description, FindingType type, SeverityLevel severity, DateTime detectionDate)
        {
            if (auditId <= 0) throw new ArgumentException("AuditId must be a positive value");
            if (string.IsNullOrWhiteSpace(description)) throw new ArgumentException("Description is required");

            return new Finding
            {
                AuditId = auditId,
                Description = description.Trim(),
                Type = type,
                Severity = severity,
                DetectionDate = detectionDate.Date
            };
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