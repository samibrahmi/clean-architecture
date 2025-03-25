using CleanArchitecture.Domain.ValueObjects;
using FluentAssertions;
using Xunit;

namespace CleanArchitecture.Domain.Tests.ValueObjects
{
    public class AddressTests
    {
        [Fact]
        public void DeuxAdressesAvecMemeValeurs_DoiventEtreEgales()
        {
            // Arrange
            var adresse1 = new Address("123 Rue Principale", "Montréal", "QC", "Canada", "H1A 1B2");
            var adresse2 = new Address("123 Rue Principale", "Montréal", "QC", "Canada", "H1A 1B2");

            // Act & Assert
            adresse1.Should().Be(adresse2);
            (adresse1 == adresse2).Should().BeTrue();
            (adresse1 != adresse2).Should().BeFalse();
            adresse1.GetHashCode().Should().Be(adresse2.GetHashCode());
        }

        [Fact]
        public void DeuxAdressesAvecValeursDifferentes_NeDoiventPasEtreEgales()
        {
            // Arrange
            var adresse1 = new Address("123 Rue Principale", "Montréal", "QC", "Canada", "H1A 1B2");
            var adresse2 = new Address("456 Rue des Chênes", "Toronto", "ON", "Canada", "M5V 2N4");

            // Act & Assert
            adresse1.Should().NotBe(adresse2);
            (adresse1 == adresse2).Should().BeFalse();
            (adresse1 != adresse2).Should().BeTrue();
            adresse1.GetHashCode().Should().NotBe(adresse2.GetHashCode());
        }

        [Fact]
        public void Adresse_CompareeAvecNull_NeDoitPasEtreEgale()
        {
            // Arrange
            var adresse = new Address("123 Rue Principale", "Montréal", "QC", "Canada", "H1A 1B2");

            // Act & Assert
            adresse.Should().NotBe(null);
            (adresse == null).Should().BeFalse();
            (adresse != null).Should().BeTrue();
            (null == adresse).Should().BeFalse();
            (null != adresse).Should().BeTrue();
        }

        [Fact]
        public void Adresse_CompareeAvecTypeDifferent_NeDoitPasEtreEgale()
        {
            // Arrange
            var adresse = new Address("123 Rue Principale", "Montréal", "QC", "Canada", "H1A 1B2");
            var obj = new object();

            // Act & Assert
            adresse.Should().NotBe(obj);
            adresse.Equals(obj).Should().BeFalse();
        }

        [Fact]
        public void Adresse_AvecUneSeuleproprieteModifiee_NeDoitPasEtreEgale()
        {
            // Arrange - seul le code postal est différent
            var adresse1 = new Address("123 Rue Principale", "Montréal", "QC", "Canada", "H1A 1B2");
            var adresse2 = new Address("123 Rue Principale", "Montréal", "QC", "Canada", "H1A 1B3");

            // Act & Assert
            adresse1.Should().NotBe(adresse2);
        }
    }
}
