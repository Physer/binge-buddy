using Application.Data;
using Application.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Caching;

public static class DependencyRegistrator
{
    public static void RegisterCachingDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<CachingOptions>(configuration.GetSection(CachingOptions.ConfigurationEntry));
        services.AddScoped<ICacher, Cacher>();
    }
}
