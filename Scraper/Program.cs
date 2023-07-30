using Application.Options;
using Persistence;
using Scraper;
using TvMaze;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        var scrapingOptionsSection = context.Configuration.GetSection(ScrapingOptions.ConfigurationEntry);
        var scrapingOptions = scrapingOptionsSection.Get<ScrapingOptions>() ?? throw new NullReferenceException("Invalid scraping options, please review your application configuration");

        services.Configure<ScrapingOptions>(scrapingOptionsSection);
        services.RegisterScraperDependencies(scrapingOptions);
        services.RegisterPersistenceDependencies(context.Configuration);
        services.AddHostedService<Worker>();
    })
    .Build();

host.Run();
