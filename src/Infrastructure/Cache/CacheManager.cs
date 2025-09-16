using Application.Abstractions.Cache;
using Microsoft.Extensions.Caching.Distributed;
using StackExchange.Redis;

namespace Infrastructure.Cache;

public class CacheManager(
    IConnectionMultiplexer redis) : ICacheManager
{
    private IDatabase? _redisDb;

    public IDatabase RedisDb => _redisDb ??= redis.GetDatabase();
    public async Task SetValueWithExpirationAsync(string key, string value, DateTime expirationUtc)
    {
        // Calculate TTL in seconds
        TimeSpan ttl = expirationUtc - DateTime.UtcNow;
        if (ttl <= TimeSpan.Zero)
        {
            return;
        }
        await RedisDb.StringSetAsync(key, value, ttl);
    }
    public async Task<string?> GetValueAsync(string key)
    {
        return await RedisDb.StringGetAsync(key);
    }
}
