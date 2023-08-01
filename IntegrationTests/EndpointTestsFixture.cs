using DotNet.Testcontainers.Builders;
using IntegrationTests.Configuration;
using Xunit;

namespace IntegrationTests;

public class EndpointTestsFixture : IAsyncLifetime
{
    public BingeWebApplicationFactory? ApplicationFactory { get; private set; }

    public Task DisposeAsync() => Task.CompletedTask;

    public async Task InitializeAsync()
    {
        var sqlServerContainer = new ContainerBuilder()
            .WithImage(SqlContainerConfiguration.ImageName)
            .WithEnvironment(SqlContainerConfiguration.EnvironmentVariables)
            .WithPortBinding(SqlContainerConfiguration.Port, true)
            .WithWaitStrategy(Wait.ForUnixContainer().UntilPortIsAvailable(SqlContainerConfiguration.Port))
            .Build();
        await sqlServerContainer.StartAsync();
        var sqlServerContainerPort = sqlServerContainer.GetMappedPublicPort(SqlContainerConfiguration.Port);

        var redisContainer = new ContainerBuilder()
            .WithImage(RedisContainerConfiguration.ImageName)
            .WithPortBinding(RedisContainerConfiguration.Port, true)
            .WithWaitStrategy(Wait.ForUnixContainer().UntilPortIsAvailable(RedisContainerConfiguration.Port))
            .Build();
        await redisContainer.StartAsync();
        var redisContainerPort = redisContainer.GetMappedPublicPort(RedisContainerConfiguration.Port);

        Dictionary<string, string?> configuration = new()
        {
            ["ConnectionStrings:BingeDatabase"] = $"Server={SqlContainerConfiguration.Hostname},{sqlServerContainerPort};Database={SqlContainerConfiguration.DatabaseName};User Id={SqlContainerConfiguration.Username};Password={SqlContainerConfiguration.Password};TrustServerCertificate=True",
            ["Caching:ConnectionString"] = $"{RedisContainerConfiguration.Hostname}:{redisContainerPort}"
        };
        ApplicationFactory = new BingeWebApplicationFactory(configuration);
    }
}
