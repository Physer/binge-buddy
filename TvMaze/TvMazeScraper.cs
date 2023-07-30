using Application.Scraping;
using Domain;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Net;
using System.Text.Json;
using TvMaze.Models;
using TvMaze.Options;

namespace TvMaze;

internal class TvMazeScraper : IShowScraper
{
    private readonly HttpClient _httpClient;
    private readonly ScrapingOptions _scrapingOptions;
    private readonly ILogger<TvMazeScraper> _logger;

    public TvMazeScraper(HttpClient httpClient,
        IOptions<ScrapingOptions> options,
        ILogger<TvMazeScraper> logger)
    {
        _httpClient = httpClient;
        _scrapingOptions = options.Value;
        _logger = logger;
    }

    public async Task<IEnumerable<Show>> ScrapeShows(int latestScrapedShowId = 0)
    {
        _logger.LogInformation("Starting scraping TV shows");
        _logger.LogInformation("Determing starting page...");

        var pageToStartFrom = Math.Floor(new decimal(latestScrapedShowId / _scrapingOptions.PageSize));
        List<Show> allShows = new();

        _logger.LogInformation("Starting scraping from page {pageToStartFrom}", pageToStartFrom);
        var page = pageToStartFrom;
        while (true)
        {
            _logger.LogInformation("Querying TVMaze API...");
            var pagedShowsResponse = await _httpClient.GetAsync($"/shows?page={pageToStartFrom}");
            if (pagedShowsResponse.StatusCode == HttpStatusCode.NotFound)
                break;

            var serializedShowData = await pagedShowsResponse.Content.ReadAsStringAsync();
            var showData = JsonSerializer.Deserialize<IEnumerable<TvMazeShow>>(serializedShowData);
            if (showData is null)
                break;

            _logger.LogInformation("Received {amountOfShows} from {page}", showData.Count(), page);
            allShows.AddRange(TvMazeMapper.Map(showData));
            page++;
        }

        _logger.LogInformation("Done querying TVMaze API. Processed {totalAmountOfShows} shows in total", allShows.Count());
        return allShows;
    }
}
