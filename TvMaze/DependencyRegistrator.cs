using Application.Options;
using Application.Scraping;
using Microsoft.Extensions.DependencyInjection;

namespace TvMaze;

public static class DependencyRegistrator
{
    public static void RegisterScraperDependencies(this IServiceCollection services, ScrapingOptions scrapingOptions) => services.AddHttpClient<IShowScraper, TvMazeScraper>(configuration => configuration.BaseAddress = new(scrapingOptions.BaseUrl!));
}
