using AutoMapper;
using CleanArchitecture.Application.Common.DTOs;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Exceptions;
using MediatR;

namespace CleanArchitecture.Application.Features.Exemples.Queries.GetExemple;

/// <summary>
/// Requête CQRS pour récupérer un seul exemple par ID
/// </summary>
public class GetExempleQuery : IRequest<ExempleDto>
{
    public Guid Id { get; set; }
}

/// <summary>
/// Gestionnaire pour la requête GetExempleQuery
/// </summary>
public class GetExempleQueryHandler : IRequestHandler<GetExempleQuery, ExempleDto>
{
    private readonly IRepository<Example> _repository;
    private readonly IMapper _mapper;

    public GetExempleQueryHandler(IRepository<Example> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<ExempleDto> Handle(GetExempleQuery request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(request.Id);
        
        if (entity == null)
        {
            throw new EntityNotFoundException(nameof(Example), request.Id);
        }
        
        return _mapper.Map<ExempleDto>(entity);
    }
}
