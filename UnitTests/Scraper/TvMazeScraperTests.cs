using Domain;
using NSubstitute;
using NSubstitute.ReceivedExtensions;
using TvMaze.Models;
using Xunit;

namespace UnitTests.Scraper;

public class TvMazeScraperTests
{
    [Theory]
    [DateOnlyAutoData]
    public async Task ScrapeShowsAsync_ScrapesFiniteNumberOfPages_CallsRepository(int pagesToScrape, IEnumerable<TvMazeShow> showData)
    {
        // Arrange
        var scraper = new TvMazeScraperBuilder()
            .WithClientReceivingNoDataAfterPage(pagesToScrape, showData)
            .Build();

        // Act
        await scraper.ScrapeShowsAsync();

        // Assert
        scraper._repository.ReceivedWithAnyArgs(pagesToScrape).AddOrUpdateShows(Arg.Any<IEnumerable<Show>>());
    }
}
