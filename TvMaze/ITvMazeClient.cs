using TvMaze.Models;

namespace TvMaze;

internal interface ITvMazeClient
{
    Task<IEnumerable<TvMazeShow>> ScrapePageAsync(int pageToScrape);
}
