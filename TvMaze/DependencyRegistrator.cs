using Application.Scraping;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TvMaze.Options;

namespace TvMaze;

public static class DependencyRegistrator
{
    public static void RegisterScraperDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        var scrapingOptionsSection = configuration.GetSection(ScrapingOptions.ConfigurationEntry);
        var scrapingOptions = scrapingOptionsSection.Get<ScrapingOptions>() ?? throw new NullReferenceException("Invalid scraping options, please review your configuration");
        services.Configure<ScrapingOptions>(scrapingOptionsSection);

        services.AddHttpClient<IShowScraper, TvMazeScraper>(configuration => configuration.BaseAddress = new(scrapingOptions.BaseUrl!));
    }
}
