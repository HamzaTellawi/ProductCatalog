using StackExchange.Redis;
using System.Text.Json;
namespace ProductCatalog.Caching.Implementations;

public class RedisCacheService
{
    private readonly IDatabase _redis;

    public RedisCacheService(IConnectionMultiplexer redis)
    {
        _redis= redis.GetDatabase(0);
    }

    public async Task<T?> GetAsync<T>(string key)
    {
        RedisValue value = await _redis.StringGetAsync(key);

        if (!value.HasValue)
            return default;

        return JsonSerializer.Deserialize<T>(value.ToString());
    }

    public async Task SetAsync<T>(string key, T value, TimeSpan expiration)
    {
        var json = JsonSerializer.Serialize(value);

        await _redis.StringSetAsync(key, json, expiration);
    }

    public async Task RemoveAsync(string key)
    {
        await _redis.KeyDeleteAsync(key);
    }
    
}