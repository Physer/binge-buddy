using Application.Data;
using Microsoft.Extensions.Logging;
using NSubstitute;
using TvMaze;
using TvMaze.Models;

namespace UnitTests.Scraper;

internal class TvMazeScraperBuilder
{
    private readonly ILogger<TvMazeScraper> _logger;
    private readonly IRepository _repository;
    private readonly ITvMazeClient _client;

    public TvMazeScraperBuilder()
    {
        _logger = Substitute.For<ILogger<TvMazeScraper>>();
        _repository = Substitute.For<IRepository>();
        _client = Substitute.For<ITvMazeClient>();
    }

    public async Task<TvMazeScraperBuilder> WithClientReturningShowDataForPage(int pageToReturnDataFor, IEnumerable<TvMazeShow> showData)
    {
        (await _client.ScrapePageAsync(pageToReturnDataFor)).Returns(showData);

        return this;
    }

    public TvMazeScraper Build() => new(_logger, _repository, _client);
}
