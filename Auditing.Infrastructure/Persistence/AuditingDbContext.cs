using Auditing.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Auditing.Infrastructure.Persistence
{
    // DbContext principal del bounded context Auditing
    public class AuditingDbContext : DbContext
    {
        public AuditingDbContext(DbContextOptions<AuditingDbContext> options) : base(options) { }

        public DbSet<Audit> Audits => Set<Audit>();
        public DbSet<Finding> Findings => Set<Finding>();
        public DbSet<Owner> Owners => Set<Owner>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Aplicar configuraciones separadas por entidad (Clean)
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AuditingDbContext).Assembly);
        }
    }
}