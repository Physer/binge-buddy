using Application.Data;
using Application.Scraping;
using Microsoft.Extensions.Logging;

namespace TvMaze;

internal class TvMazeScraper : IShowScraper
{
    private readonly ILogger<TvMazeScraper> _logger;
    private readonly IRepository _repository;
    private readonly TvMazeClient _client;

    public TvMazeScraper(ILogger<TvMazeScraper> logger,
        IRepository repository,
        TvMazeClient client)
    {
        _logger = logger;
        _repository = repository;
        _client = client;
    }

    public async Task ScrapeShowsAsync()
    {
        _logger.LogInformation("Starting scraping TV shows");
        var currentPage = 0;
        var finishedScraping = false;
        while (!finishedScraping)
        {
            var showData = await _client.ScrapePage(currentPage);
            if (!showData.Any())
                break;

            var shows = TvMazeMapper.Map(showData);
            await _repository.AddOrUpdateShowsAsync(shows);
            currentPage++;
        }
    }
}
