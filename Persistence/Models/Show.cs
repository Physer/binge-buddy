namespace Persistence.Models;

internal sealed class Show
{
    public required Guid Id { get; init; }
    public required int ExternalId { get; init; }
    public required string Name { get; init; }
    public required string Language { get; init; }
    public DateTime? Premiered { get; init; }
    public DateTime? Ended { get; init; }
    public string? Genres { get; init; }
    public string? Summary { get; init; }
    public required DateTime UpdatedAt { get; init; }
}
