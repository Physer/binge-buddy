using Domain;

namespace Application.Scraping;

public interface IShowScraper
{
    Task<IEnumerable<Show>> ScrapeShows(int latestScrapedShowId = 0);
}
