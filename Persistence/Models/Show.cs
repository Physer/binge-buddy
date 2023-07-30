namespace Persistence.Models;

internal sealed class Show
{
    public Guid Id { get; init; }
    public int ExternalId { get; set; }
    public string? Name { get; set; }
    public string? Language { get; set; }
    public DateTime Premiered { get; set; }
    public DateTime? Ended { get; set; }
    public string? Genres { get; set; }
    public string? Summary { get; set; }
}
