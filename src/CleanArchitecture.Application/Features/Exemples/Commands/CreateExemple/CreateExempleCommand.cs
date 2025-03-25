using AutoMapper;
using CleanArchitecture.Application.Common.DTOs;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Domain.Entities;

namespace CleanArchitecture.Application.Features.Exemples.Commands.CreateExemple;

/// <summary>
/// Commande CQRS pour créer un nouvel exemple
/// </summary>
public class CreateExempleCommand : IRequest<ExempleDto>
{
    public string Nom { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}

/// <summary>
/// Validateur pour la commande CreateExempleCommand
/// </summary>
public class CreateExempleCommandValidator : AbstractValidator<CreateExempleCommand>
{
    public CreateExempleCommandValidator()
    {
        RuleFor(x => x.Nom)
            .NotEmpty().WithMessage("Le nom est obligatoire.")
            .MaximumLength(100).WithMessage("Le nom ne doit pas dépasser 100 caractères.");

        RuleFor(x => x.Description)
            .MaximumLength(500).WithMessage("La description ne doit pas dépasser 500 caractères.");
    }
}

/// <summary>
/// Gestionnaire pour la commande CreateExempleCommand
/// </summary>
public class CreateExempleCommandHandler : IRequestHandler<CreateExempleCommand, ExempleDto>
{
    private readonly IRepository<Example> _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateExempleCommandHandler(
        IRepository<Example> repository,
        IUnitOfWork unitOfWork,
        IMapper mapper)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<ExempleDto> Handle(CreateExempleCommand request, CancellationToken cancellationToken)
    {
        var entity = new Example(request.Nom, request.Description);
        
        var savedEntity = await _repository.AddAsync(entity);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        
        return _mapper.Map<ExempleDto>(savedEntity);
    }
}
