using CleanArchitecture.Domain.ValueObjects;

namespace CleanArchitecture.Domain.Entities
{
    /// <summary>
    /// Exemple d'entité démontrant les modèles d'entité de domaine
    /// </summary>
    public class Example : Entity
    {
        public string Name { get; private set; } = null!;
        public string Description { get; private set; } = null!;
        public Status Status { get; private set; }
        public DateTime CreatedAt { get; private set; }

        private Example() 
        {
            // Initialiser les valeurs par défaut pour le constructeur EF Core
            CreatedAt = DateTime.UtcNow;
            Status = Status.Inactive;
        }

        public Example(string name, string description)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Name cannot be empty", nameof(name));

            Name = name;
            Description = description;
            Status = Status.Active;
            CreatedAt = DateTime.UtcNow;
        }

        public void Update(string name, string description)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Name cannot be empty", nameof(name));

            Name = name;
            Description = description;
        }

        public void Deactivate()
        {
            Status = Status.Inactive;
        }

        public void Activate()
        {
            Status = Status.Active;
        }
    }
}
