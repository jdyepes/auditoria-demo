using Auditing.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Auditing.Infrastructure.Persistence.Configurations
{
    // Configuración de la entidad Audit (mapeos, índices y FKs)
    public class AuditConfiguration : IEntityTypeConfiguration<Audit>
    {
        public void Configure(EntityTypeBuilder<Audit> e)
        {
            e.ToTable("Audits");

            e.HasKey(x => x.Id);

            e.Property(x => x.Title)
             .HasMaxLength(200)
             .IsRequired();

            e.Property(x => x.AuditedArea)
             .HasMaxLength(150)
             .IsRequired();

            e.Property(x => x.StartDate)
             .HasColumnType("date")
             .IsRequired();

            e.Property(x => x.EndDate)
             .HasColumnType("date")
             .IsRequired();

            e.Property(x => x.Status)
             .IsRequired();

            e.Property(x => x.CreatedAtUtc)
             .HasColumnType("datetime2")
             .IsRequired();

            e.Property(x => x.UpdatedAtUtc)
             .HasColumnType("datetime2");

            e.HasOne(x => x.Owner)
             .WithMany()
             .HasForeignKey(x => x.OwnerId)
             .OnDelete(DeleteBehavior.Restrict);

            e.HasIndex(x => new { x.StartDate, x.EndDate, x.Status })
             .HasDatabaseName("IX_Audits_DateStatus");
        }
    }
}