using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Persistence;

namespace IntegrationTests;

public class BingeWebApplicationFactory : WebApplicationFactory<Program>
{
    private readonly IDictionary<string, string?> _configuration;

    public BingeWebApplicationFactory(IDictionary<string, string?> configuration) => _configuration = configuration;

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        var inMemoryConfiguration = new ConfigurationBuilder().AddInMemoryCollection(_configuration).Build();
        builder.UseConfiguration(inMemoryConfiguration);
        builder.ConfigureServices(services => services.RegisterPersistenceDependencies(inMemoryConfiguration));
    }
}
