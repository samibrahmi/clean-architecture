using AutoMapper;
using CleanArchitecture.Application.Common.DTOs;
using CleanArchitecture.Application.Common.Mappings;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.ValueObjects;
using FluentAssertions;
using Xunit;

namespace CleanArchitecture.Application.Tests.Common.Mappings
{
    public class MappingProfileTests
    {
        private readonly IConfigurationProvider _configuration;
        private readonly IMapper _mapper;

        public MappingProfileTests()
        {
            _configuration = new MapperConfiguration(config =>
                config.AddProfile<MappingProfile>());

            _mapper = _configuration.CreateMapper();
        }

        [Fact]
        public void ConfigurationDevraitEtreValide()
        {
            // Vérifie que la configuration de mapping est valide
            _configuration.AssertConfigurationIsValid();
        }

        [Fact]
        public void ExampleVersExempleDto_DevraitMapperCorrectement()
        {
            // Arrangement
            var example = new Example("Nom de test", "Description de test");
            
            // Action
            var dto = _mapper.Map<ExempleDto>(example);
            
            // Assertion
            dto.Should().NotBeNull();
            dto.Nom.Should().Be("Nom de test");
            dto.Description.Should().Be("Description de test");
            dto.Statut.Should().Be(Status.Active.ToString());
        }
    }
}
