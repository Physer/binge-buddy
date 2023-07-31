using Persistence;
using Scraper;
using TvMaze;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        services.RegisterScraperDependencies(context.Configuration);
        services.RegisterPersistenceDependencies(context.Configuration);
        services.AddHostedService<Worker>();
    })
    .Build();

host.Run();
