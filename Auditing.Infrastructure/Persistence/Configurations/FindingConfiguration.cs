using Auditing.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Auditing.Infrastructure.Persistence.Configurations
{
    // Configuración de la entidad Finding
    public class FindingConfiguration : IEntityTypeConfiguration<Finding>
    {
        public void Configure(EntityTypeBuilder<Finding> e)
        {
            e.ToTable("Findings");

            e.HasKey(x => x.Id);

            e.Property(x => x.Description)
             .HasMaxLength(500)
             .IsRequired();

            e.Property(x => x.DetectionDate)
             .HasColumnType("date")
             .IsRequired();

            e.Property(x => x.CreatedAtUtc)
             .HasColumnType("datetime2")
             .IsRequired();

            e.HasOne(x => x.Audit)
             .WithMany(a => a.Findings)
             .HasForeignKey(x => x.AuditId)
             .OnDelete(DeleteBehavior.Cascade);

            e.HasIndex(x => new { x.AuditId, x.Severity })
             .HasDatabaseName("IX_Findings_AuditSeverity");
        }
    }
}