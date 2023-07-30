namespace Domain;

public record struct Show(string Name, string Language, DateOnly? Premiered, DateOnly? Ended, IEnumerable<string> Genres, string Summary);
