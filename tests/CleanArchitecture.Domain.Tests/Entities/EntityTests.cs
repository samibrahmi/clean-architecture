using CleanArchitecture.Domain.Entities;
using FluentAssertions;
using Xunit;
using System;

namespace CleanArchitecture.Domain.Tests.Entities
{
    public class EntityTests
    {
        // Classe concrète pour tester la classe abstraite Entity
        private class TestEntity : Entity
        {
            public TestEntity() 
            {
                // Assurez-vous que l'ID est initialisé correctement
                // en appelant explicitement la méthode de génération d'ID
                InitializeId();
            }

            // Méthode pour forcer l'initialisation de l'ID si nécessaire
            private void InitializeId()
            {
                if (Id == Guid.Empty)
                {
                    // Utiliser la réflexion pour définir l'ID si la propriété est accessible
                    var idProperty = typeof(Entity).GetProperty("Id");
                    if (idProperty != null && idProperty.CanWrite)
                    {
                        idProperty.SetValue(this, Guid.NewGuid());
                    }
                }
            }

            // Propriété spécifique pour les tests
            public string? TestProperty { get; set; }
        }

        [Fact]
        public void Entity_WithDifferentIds_ShouldNotBeEqual()
        {
            // Arrange
            var entity1 = new TestEntity();
            var entity2 = new TestEntity();

            // Garantir que les IDs sont différents et non vides
            entity1.Id.Should().NotBe(Guid.Empty, "l'ID de l'entité ne devrait pas être vide");
            entity2.Id.Should().NotBe(Guid.Empty, "l'ID de l'entité ne devrait pas être vide");
            entity1.Id.Should().NotBe(entity2.Id, "les IDs des entités devraient être différents");

            // Act & Assert
            entity1.Should().NotBe(entity2);
            (entity1 == entity2).Should().BeFalse();
            (entity1 != entity2).Should().BeTrue();
        }

        [Fact]
        public void Entity_WithSameReference_ShouldBeEqual()
        {
            // Arrange
            var entity1 = new TestEntity();
            var entity2 = entity1;

            // Act & Assert
            entity1.Should().Be(entity2);
            (entity1 == entity2).Should().BeTrue();
            (entity1 != entity2).Should().BeFalse();
        }

        [Fact]
        public void Entity_ComparedWithNull_ShouldNotBeEqual()
        {
            // Arrange
            var entity = new TestEntity();

            // Act & Assert
            entity.Should().NotBe(null);
            (entity == null).Should().BeFalse();
            (entity != null).Should().BeTrue();
            (null == entity).Should().BeFalse();
            (null != entity).Should().BeTrue();
        }

        [Fact]
        public void Entity_ComparedWithDifferentType_ShouldNotBeEqual()
        {
            // Arrange
            var entity = new TestEntity();
            var obj = new object();

            // Act & Assert
            entity.Should().NotBe(obj);
            entity.Equals(obj).Should().BeFalse();
        }

        [Fact]
        public void Entity_WithSameId_ShouldBeEqual()
        {
            // Arrange
            var entity1 = new TestEntity();
            
            // Créer une entité avec la même ID en utilisant la réflexion
            var entity2 = new TestEntity();
            var idProperty = typeof(Entity).GetProperty("Id");
            idProperty?.SetValue(entity2, idProperty.GetValue(entity1));

            // Act & Assert
            entity1.Should().Be(entity2);
            (entity1 == entity2).Should().BeTrue();
            (entity1 != entity2).Should().BeFalse();
            entity1.GetHashCode().Should().Be(entity2.GetHashCode());
        }
    }
}
