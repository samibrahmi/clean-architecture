namespace CleanArchitecture.Application.Common.DTOs;

/// <summary>
/// DTO d'exemple pour transfert de données
/// </summary>
public class ExempleDto
{
    public Guid Id { get; set; }
    public string Nom { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime DateCreation { get; set; }
    public string Statut { get; set; } = string.Empty;
}
