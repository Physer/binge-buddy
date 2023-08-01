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

    public TvMazeScraperBuilder WithClientReceivingNoDataAfterPage(int pageToStopReceivingDataAt, IEnumerable<TvMazeShow> dataToReceive)
    {
        _client.ScrapePageAsync(Arg.Any<int>()).Returns(dataToReceive);
        _client.ScrapePageAsync(pageToStopReceivingDataAt).Returns(Enumerable.Empty<TvMazeShow>());

        return this;
    }

    public TvMazeScraper Build() => new(_logger, _repository, _client);
}
