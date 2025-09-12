using Application.Abstractions.Cache;
using StackExchange.Redis;

namespace Infrastructure.Cache;

public class CacheManager(IConnectionMultiplexer redis) : ICacheManager
{
    private IDatabase? _redisDb;

    public IDatabase RedisDb => _redisDb ??= redis.GetDatabase();
}
