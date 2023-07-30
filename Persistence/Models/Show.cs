namespace Persistence.Models;

internal sealed class Show
{
    public Guid Id { get; set; }
    public required int ExternalId { get; init; }
    public string? Name { get; init; }
    public string? Language { get; init; }
    public DateTime? Premiered { get; init; }
    public DateTime? Ended { get; init; }
    public string? Genres { get; init; }
    public string? Summary { get; init; }
    public required DateTime UpdatedAt { get; init; }
}
