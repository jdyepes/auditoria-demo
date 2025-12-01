
namespace Auditing.Domain.Entities
{
    // Responsable de auditoría (email debe ser único en persistencia)
    public class Owner
    {
        public int Id { get; private set; }                       // Id responsable
        public string Name { get; private set; } = default!;      // Nombre
        public string Email { get; private set; } = default!;     // Email único
        public string Area { get; private set; } = default!;      // Área
        public DateTime CreatedAtUtc { get; private set; } = DateTime.UtcNow; // Creación UTC

        // Fábrica con validaciones básicas
        public static Owner Create(string name, string email, string area)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Name is required");
            if (string.IsNullOrWhiteSpace(email)) throw new ArgumentException("Email is required");
            if (string.IsNullOrWhiteSpace(area)) throw new ArgumentException("Area is required");

            return new Owner
            {
                Name = name.Trim(),
                Email = email.Trim(),
                Area = area.Trim()
            };
        }

        public void Update(string name, string email, string area)
        {
            Name = name.Trim();
            Email = email.Trim().ToLower();
            Area = area.Trim();
        }

    }
}