namespace TvMaze.Models;

internal sealed record TvMazeShow(int Id, string Name, string? Language, IEnumerable<string> Genres, DateOnly? Premiered, DateOnly? Ended, string? Summary);