using AutoMapper;
using CleanArchitecture.Application.Common.DTOs;
using CleanArchitecture.Domain.Entities;

namespace CleanArchitecture.Application.Common.Mappings;

/// <summary>
/// Profil AutoMapper pour configurer les mappages entre entités et DTOs
/// </summary>
public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Configurer les mappages ici
        CreateMap<Example, ExempleDto>()
            .ForMember(dest => dest.Nom, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.DateCreation, opt => opt.MapFrom(src => src.CreatedAt))
            .ForMember(dest => dest.Statut, opt => opt.MapFrom(src => src.Status.ToString()));
    }
}
