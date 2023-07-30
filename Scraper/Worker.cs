using Application.Options;
using Application.Scraping;
using Microsoft.Extensions.Options;
using System.Diagnostics;

namespace Scraper;

public sealed class Worker : IHostedService, IDisposable
{
    private readonly ILogger<Worker> _logger;
    private readonly ScrapingOptions _scrapingOptions;
    private readonly IServiceProvider _serviceProvider;

    private Timer? _timer = null;

    public Worker(ILogger<Worker> logger,
        IOptions<ScrapingOptions> options,
        IServiceProvider serviceProvider)
    {
        _logger = logger;
        _scrapingOptions = options.Value;
        _serviceProvider = serviceProvider;
    }

    public Task StartAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Starting Scraper worker service, triggering every {interval} seconds", _scrapingOptions.SynchronizationIntervalInSeconds);
        _timer = new Timer(Scrape, null, TimeSpan.Zero, TimeSpan.FromSeconds(_scrapingOptions.SynchronizationIntervalInSeconds));
        return Task.CompletedTask;
    }

    private async void Scrape(object? state)
    {
        var stopwatch = Stopwatch.StartNew();
        _logger.LogInformation("Scraping at {timing} UTC", DateTime.UtcNow.ToLongTimeString());
        using var scope = _serviceProvider.CreateScope();
        var showScraper = scope.ServiceProvider.GetRequiredService<IShowScraper>();
        await showScraper.ScrapeShowsAsync();
        stopwatch.Stop();
        _logger.LogInformation("Finished scraping in {elapsedMiliseconds} miliseconds", stopwatch.ElapsedMilliseconds);
    }

    public Task StopAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Stopping the Scraper worker service.");
        _timer?.Change(Timeout.Infinite, 0);
        return Task.CompletedTask;
    }

    public void Dispose() => _timer?.Dispose();
}