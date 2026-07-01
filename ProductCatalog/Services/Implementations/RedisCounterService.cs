using ProductCatalog.Services.Interfaces;
using StackExchange.Redis;

namespace ProductCatalog.Services.Implementations;

public class RedisCounterService : IRedisService
{
    private readonly IDatabase _database;

    public RedisCounterService(IConnectionMultiplexer redis)
    {
        _database = redis.GetDatabase();
    }

    public async Task<long> IncrementAsync(string key)
    {
        return await _database.StringIncrementAsync(key);
    }

    public async Task<long> GetLongAsync(string key)
    {
        var value = await _database.StringGetAsync(key);

        if (value.IsNull)
            return 0;

        return (long)value;
    }
}