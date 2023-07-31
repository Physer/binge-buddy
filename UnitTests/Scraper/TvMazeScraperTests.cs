using TvMaze;

namespace UnitTests.Scraper;

public class TvMazeScraperTests
{
    public async Task ScrapeShowsAsync_ScrapesFiniteNumberOfPages_CallsRepository()
    {
        // Arrange
        var scraper = new TvMazeScraperBuilder().Build();

        // Act
        await scraper.ScrapeShowsAsync();

        // Assert

    }
}
