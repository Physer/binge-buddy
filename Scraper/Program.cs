using Persistence;
using Scraper;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        services.RegisterPersistenceDependencies(context.Configuration);

        services.AddHostedService<Worker>();
    })
    .Build();

host.Run();
