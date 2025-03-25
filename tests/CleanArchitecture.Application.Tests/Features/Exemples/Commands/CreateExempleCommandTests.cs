using AutoMapper;
using CleanArchitecture.Application.Common.DTOs;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Application.Features.Exemples.Commands.CreateExemple;
using CleanArchitecture.Domain.Entities;
using FluentAssertions;
using Moq;
using Xunit;

namespace CleanArchitecture.Application.Tests.Features.Exemples.Commands
{
    public class CreateExempleCommandTests
    {
        private readonly Mock<IRepository<Example>> _repositoryMock;
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly Mock<IMapper> _mapperMock;
        
        public CreateExempleCommandTests()
        {
            _repositoryMock = new Mock<IRepository<Example>>();
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _mapperMock = new Mock<IMapper>();
        }
        
        [Fact]
        public async Task Handle_AvecCommandeValide_DoitCreerExempleEtRetournerDto()
        {
            // Arrangement
            var command = new CreateExempleCommand
            {
                Nom = "Exemple de test",
                Description = "Description de test"
            };
            
            var entity = new Example("Exemple de test", "Description de test");
            var expectedDto = new ExempleDto
            {
                Id = Guid.NewGuid(),
                Nom = "Exemple de test",
                Description = "Description de test",
                Statut = "Active"
            };
            
            _repositoryMock
                .Setup(r => r.AddAsync(It.IsAny<Example>()))
                .ReturnsAsync(entity);
                
            _mapperMock
                .Setup(m => m.Map<ExempleDto>(It.IsAny<Example>()))
                .Returns(expectedDto);
                
            var handler = new CreateExempleCommandHandler(
                _repositoryMock.Object,
                _unitOfWorkMock.Object,
                _mapperMock.Object);
                
            // Action
            var result = await handler.Handle(command, CancellationToken.None);
            
            // Assertion
            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(expectedDto);
            
            _repositoryMock.Verify(r => r.AddAsync(It.Is<Example>(e => 
                e.Name == command.Nom && 
                e.Description == command.Description)), Times.Once);
                
            _unitOfWorkMock.Verify(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }
        
        [Fact]
        public async Task ValidateurCommande_AvecNomVide_DoitEchouer()
        {
            // Arrangement
            var validator = new CreateExempleCommandValidator();
            var command = new CreateExempleCommand
            {
                Nom = "",
                Description = "Description valide"
            };
            
            // Action
            var result = await validator.ValidateAsync(command);
            
            // Assertion
            result.IsValid.Should().BeFalse();
            result.Errors.Should().ContainSingle(e => e.PropertyName == nameof(command.Nom));
        }
    }
}
