using Application.Abstractions.Service;
using StackExchange.Redis;

namespace Application.Abstractions.Cache;

public interface ICacheManager : IService
{
    IDatabase RedisDb { get; }
    Task SetValueWithExpirationAsync(string key, string value, DateTime expirationUtc);
    Task<string?> GetValueAsync(string key);
}
