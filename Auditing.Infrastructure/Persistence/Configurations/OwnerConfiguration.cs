using Auditing.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Auditing.Infrastructure.Persistence.Configurations
{
    // Configuración de la entidad Owner
    public class OwnerConfiguration : IEntityTypeConfiguration<Owner>
    {
        public void Configure(EntityTypeBuilder<Owner> e)
        {
            e.ToTable("Owners");

            e.HasKey(x => x.Id);

            e.Property(x => x.Name)
             .HasMaxLength(150)
             .IsRequired();

            e.Property(x => x.Email)
             .HasMaxLength(150)
             .IsRequired();

            e.HasIndex(x => x.Email)
             .IsUnique();

            e.Property(x => x.Area)
             .HasMaxLength(100)
             .IsRequired();

            e.Property(x => x.CreatedAtUtc)
             .HasColumnType("datetime2")
             .IsRequired();
        }
    }
}