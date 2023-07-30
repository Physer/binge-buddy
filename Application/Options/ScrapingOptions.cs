namespace Application.Options;

public class ScrapingOptions
{
    public const string ConfigurationEntry = "Scraping";

    public string? BaseUrl { get; set; }
    public int PageSize { get; set; }
    public int SynchronizationIntervalInSeconds { get; set; }
}
