using Application.Data;
using Application.Options;
using Microsoft.Extensions.Options;
using StackExchange.Redis;
using System.Text.Json;

namespace Caching;

internal class Cacher : ICacher
{
    private readonly IDatabase _database;

    public Cacher(IOptions<CachingOptions> options)
    {
        var cachingOptions = options.Value;
        var redisConnection = ConnectionMultiplexer.Connect(cachingOptions?.ConnectionString!);
        _database = redisConnection.GetDatabase();
    }

    public async Task<T?> GetAsync<T>(string cacheKey)
    {
        var cachedValue = await _database.StringGetAsync(cacheKey);
        return !cachedValue.HasValue ? default : JsonSerializer.Deserialize<T>(cachedValue!, new JsonSerializerOptions(JsonSerializerDefaults.Web));
    }

    public void Store<T>(string cacheKey, T input)
    {
        var dataToCache = JsonSerializer.Serialize(input, new JsonSerializerOptions(JsonSerializerDefaults.Web));
        _database.StringSetAsync(cacheKey, dataToCache, TimeSpan.FromHours(1));
    }
}
