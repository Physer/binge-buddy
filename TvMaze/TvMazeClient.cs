using Microsoft.Extensions.Logging;
using System.Net;
using System.Text.Json;
using TvMaze.Models;

namespace TvMaze;

internal class TvMazeClient
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<TvMazeClient> _logger;

    public TvMazeClient(HttpClient httpClient,
        ILogger<TvMazeClient> logger)
    {
        _httpClient = httpClient;
        _logger = logger;
    }

    public async Task<IEnumerable<TvMazeShow>> ScrapePage(int pageToScrape)
    {
        _logger.LogInformation("Querying TVMaze API...");
        var pagedShowsResponse = await _httpClient.GetAsync($"/shows?page={pageToScrape}");
        if (pagedShowsResponse.StatusCode == HttpStatusCode.NotFound)
            return Enumerable.Empty<TvMazeShow>();

        var serializedShowData = await pagedShowsResponse.Content.ReadAsStringAsync();
        var showData = JsonSerializer.Deserialize<IEnumerable<TvMazeShow>>(serializedShowData, new JsonSerializerOptions(JsonSerializerDefaults.Web));
        _logger.LogInformation("Received {amountOfShows} from {page}", showData?.Count(), pageToScrape);

        return showData ?? Enumerable.Empty<TvMazeShow>();
    }
}
