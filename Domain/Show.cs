namespace Domain;

public record struct Show(int ExternalId, string Name, string? Language, DateOnly? Premiered, DateOnly? Ended, IEnumerable<string> Genres, string? Summary);
