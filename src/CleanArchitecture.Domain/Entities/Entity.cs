using CleanArchitecture.Domain.Common;

namespace CleanArchitecture.Domain.Entities
{
    /// <summary>
    /// Classe de base pour toutes les entités de domaine
    /// </summary>
    public abstract class Entity : EntityBase
    {
        protected Entity()
        {
            // Initialiser explicitement l'ID avec une nouvelle GUID
            Id = Guid.NewGuid();
        }

        public override bool Equals(object? obj)
        {
            var other = obj as Entity;
            if (other is null)
                return false;
            
            if (ReferenceEquals(this, other))
                return true;

            return Id.Equals(other.Id);
        }

        public override int GetHashCode() => Id.GetHashCode();

        public static bool operator ==(Entity? left, Entity? right)
        {
            if (left is null && right is null)
                return true;
            
            if (left is null || right is null)
                return false;
                
            return left.Equals(right);
        }

        public static bool operator !=(Entity? left, Entity? right) => !(left == right);
    }
}
