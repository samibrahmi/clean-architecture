namespace CleanArchitecture.Domain.ValueObjects
{
    /// <summary>
    /// Exemple d'objet-valeur représentant une adresse
    /// </summary>
    public class Address : ValueObject
    {
        public string Street { get; private set; } = null!;
        public string City { get; private set; } = null!;
        public string State { get; private set; } = null!;
        public string Country { get; private set; } = null!;
        public string PostalCode { get; private set; } = null!;

        private Address() 
        {
            // Constructeur vide pour EF Core
        }

        public Address(string street, string city, string state, string country, string postalCode)
        {
            Street = street;
            City = city;
            State = state;
            Country = country;
            PostalCode = postalCode;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Street;
            yield return City;
            yield return State;
            yield return Country;
            yield return PostalCode;
        }
    }
}
