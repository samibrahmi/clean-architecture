namespace CleanArchitecture.Domain.Common;

/// <summary>
/// Classe de base pour toutes les entités du domaine
/// </summary>
public abstract class EntityBase
{
    public Guid Id { get; protected set; }
    public DateTime CreatedOn { get; private set; }
    public string CreatedBy { get; private set; } = string.Empty;
    public DateTime? LastModifiedOn { get; private set; }
    public string LastModifiedBy { get; private set; } = string.Empty;

    public void SetCreationInfo(string createdBy)
    {
        CreatedOn = DateTime.UtcNow;
        CreatedBy = createdBy;
    }

    public void SetModificationInfo(string modifiedBy)
    {
        LastModifiedOn = DateTime.UtcNow;
        LastModifiedBy = modifiedBy;
    }
}
