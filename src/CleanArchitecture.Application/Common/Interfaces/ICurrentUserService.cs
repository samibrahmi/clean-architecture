namespace CleanArchitecture.Application.Common.Interfaces;

/// <summary>
/// Interface pour accéder à l'utilisateur actuel
/// </summary>
public interface ICurrentUserService
{
    string? UserId { get; }
}
