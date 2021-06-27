using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using StackExchange.Redis;

namespace Neo.Extensions.Redis
{
    public interface IRedisService
    {
        Task<bool> KeyExistsAsync(string redisKey, RedisPath nameKey);
        Task<TimeSpan?> KeyIdleTimeAsync(string redisKey, RedisPath nameKey);
        Task<bool> KeyExpireAsync(string redisKey, TimeSpan? expiry, RedisPath nameKey);
        Task<bool> KeyDeleteAsync(string redisKey, RedisPath nameKey);
        Task<bool> StringSetAsync(string redisKey, string redisValue, RedisPath nameKey, TimeSpan? expiry = null);
        Task<bool> StringSetAsync(string redisKey, RedisValue redisValue, RedisPath nameKey, TimeSpan? expiry = null);
        Task<bool> StringSetAsync<T>(string redisKey, T redisValue, RedisPath nameKey, TimeSpan? expiry = null);
        Task<bool> StringSetAsync(IEnumerable<KeyValuePair<RedisKey, RedisValue>> keyValuePairs, RedisPath nameKey);
        Task<string> StringGetAsync(string redisKey, RedisPath nameKey);
        Task<bool> LockTakeAsync(RedisKey key, RedisValue value, TimeSpan expiry, CommandFlags flags = CommandFlags.None);
        Task<bool> LockReleaseAsync(RedisKey key, RedisValue value, CommandFlags flags = CommandFlags.None);
        Task<bool> GeoAddAsync(RedisKey key, GeoEntry value, CommandFlags flags = CommandFlags.None);
        Task<bool> GeoRemoveAsync(RedisKey key, RedisValue member, CommandFlags flags = CommandFlags.None);
        Task<bool> HashSetAsync(RedisKey key, RedisValue hashField, RedisValue value, When when = When.Always, CommandFlags flags = CommandFlags.None);
        Task<long> PublishAsync(RedisChannel channel, RedisValue message, CommandFlags flags = CommandFlags.None);
        T JsonHashGet<T>(string hashField, string key);
        T StringGet<T>(string redisKey, RedisPath nameKey);
    }
}
