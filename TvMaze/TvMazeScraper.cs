using Application.Data;
using Application.Scraping;
using Microsoft.Extensions.Logging;

namespace TvMaze;

internal class TvMazeScraper : IShowScraper
{
    private readonly ILogger<TvMazeScraper> _logger;
    internal readonly IRepository _repository;
    private readonly ITvMazeClient _client;

    public TvMazeScraper(ILogger<TvMazeScraper> logger,
        IRepository repository,
        ITvMazeClient client)
    {
        _logger = logger;
        _repository = repository;
        _client = client;
    }

    public async Task ScrapeShowsAsync()
    {
        _logger.LogInformation("Starting scraping TV shows");
        var currentPage = 0;
        while (true)
        {
            var showData = await _client.ScrapePageAsync(currentPage);
            if (!showData.Any())
                break;

            var shows = TvMazeMapper.Map(showData);
            _repository.AddOrUpdateShows(shows.Where(show => show.Premiered.HasValue && show.Premiered.Value > new DateOnly(2014, 01, 01)));
            currentPage++;
        }
    }
}
