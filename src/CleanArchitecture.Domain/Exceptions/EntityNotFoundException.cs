namespace CleanArchitecture.Domain.Exceptions
{
    /// <summary>
    /// Exception levée quand une entité n'est pas trouvée
    /// </summary>
    public class EntityNotFoundException : DomainException
    {
        public EntityNotFoundException(string entityName, object key)
            : base($"L'entité {entityName} avec la clé {key} n'a pas été trouvée.")
        {
            EntityName = entityName;
            Key = key;
        }

        public string EntityName { get; }
        public object Key { get; }
    }
}
