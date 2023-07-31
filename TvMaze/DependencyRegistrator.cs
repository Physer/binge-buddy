using Application.Options;
using Application.Scraping;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace TvMaze;

public static class DependencyRegistrator
{
    public static void RegisterScraperDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        var scrapingOptionsSection = configuration.GetSection(ScrapingOptions.ConfigurationEntry);
        var scrapingOptions = scrapingOptionsSection.Get<ScrapingOptions>() ?? throw new NullReferenceException("Invalid scraping options, please review your application configuration");
        services.Configure<ScrapingOptions>(scrapingOptionsSection);

        services.AddHttpClient<TvMazeClient>(configuration => configuration.BaseAddress = new(scrapingOptions.BaseUrl!));
        services.AddScoped<IShowScraper, TvMazeScraper>();
    }
}
