namespace IntegrationTests.Configuration;

internal class RedisContainerConfiguration
{
    public static string Hostname => "localhost";
    public static int Port => 6379;

    public static string ImageName => "redis:latest";
}
