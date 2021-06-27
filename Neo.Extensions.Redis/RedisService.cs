using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StackExchange.Redis;
using Utf8Json;
using Utf8Json.Resolvers;

namespace Neo.Extensions.Redis
{
    public class RedisService : IRedisService
    {
        public readonly IConnectionMultiplexer ConnMultiplexer;

        public RedisService(IConnectionMultiplexer connMultiplexer)
        {
            ConnMultiplexer = connMultiplexer;
        }

        public IDatabase GetDatabase()
        {
            return ConnMultiplexer.GetDatabase();
        }

        public async Task<bool> KeyExistsAsync(string redisKey, RedisPath nameKey)
        {
            return await GetDatabase().KeyExistsAsync($"{nameKey.Value}:{redisKey}");
        }

        public async Task<TimeSpan?> KeyIdleTimeAsync(string redisKey, RedisPath nameKey)
        {
            return await GetDatabase().KeyIdleTimeAsync($"{nameKey.Value}:{redisKey}");
        }

        public async Task<bool> KeyExpireAsync(string redisKey, TimeSpan? expiry, RedisPath nameKey)
        {
            return await GetDatabase().KeyExpireAsync($"{nameKey.Value}:{redisKey}", expiry);
        }

        public async Task<bool> KeyDeleteAsync(string redisKey, RedisPath nameKey)
        {
            return await GetDatabase().KeyDeleteAsync($"{nameKey.Value}:{redisKey}");
        }

        public async Task<bool> StringSetAsync(string redisKey, string redisValue, RedisPath nameKey, TimeSpan? expiry = null)
        {
            return await GetDatabase().StringSetAsync($"{nameKey.Value}:{redisKey}", redisValue, expiry);
        }

        public async Task<bool> StringSetAsync(string redisKey, RedisValue redisValue, RedisPath nameKey, TimeSpan? expiry = null)
        {
            return await GetDatabase().StringSetAsync($"{nameKey.Value}:{redisKey}", redisValue, expiry);
        }

        public async Task<bool> StringSetAsync<T>(string redisKey, T redisValue, RedisPath nameKey, TimeSpan? expiry = null)
        {
            return await GetDatabase().StringSetAsync($"{nameKey.Value}:{redisKey}", Serialize(redisValue), expiry);
        }

        public async Task<bool> StringSetAsync(IEnumerable<KeyValuePair<RedisKey, RedisValue>> keyValuePairs, RedisPath nameKey)
        {
            keyValuePairs = keyValuePairs.Select(kv => new KeyValuePair<RedisKey, RedisValue>(kv.Key, kv.Value));

            return await GetDatabase().StringSetAsync(keyValuePairs.ToArray());
        }

        public async Task<string> StringGetAsync(string redisKey, RedisPath nameKey)
        {
            return await GetDatabase().StringGetAsync($"{nameKey.Value}:{redisKey}");
        }

        public async Task<bool> LockTakeAsync(RedisKey key, RedisValue value, TimeSpan expiry, CommandFlags flags = CommandFlags.None)
        {
            return await GetDatabase().LockTakeAsync(key, value, expiry, flags);
        }

        public async Task<bool> LockReleaseAsync(RedisKey key, RedisValue value, CommandFlags flags = CommandFlags.None)
        {
            return await GetDatabase().LockReleaseAsync(key, value, flags);
        }

        public async Task<bool> GeoAddAsync(RedisKey key, GeoEntry value, CommandFlags flags = CommandFlags.None)
        {
            return await GetDatabase().GeoAddAsync(key, value, flags);
        }

        public async Task<bool> GeoRemoveAsync(RedisKey key, RedisValue member, CommandFlags flags = CommandFlags.None)
        {
            return await GetDatabase().GeoRemoveAsync(key, member, flags);
        }

        public async Task<bool> HashSetAsync(RedisKey key, RedisValue hashField, RedisValue value, When when = When.Always, CommandFlags flags = CommandFlags.None)
        {
            return await GetDatabase().HashSetAsync(key, hashField, value, when, flags);
        }

        public async Task<long> PublishAsync(RedisChannel channel, RedisValue message, CommandFlags flags = CommandFlags.None)
        {
            return await GetDatabase().PublishAsync(channel, message, flags);
        }

        public T JsonHashGet<T>(string hashField, string key)
        {
            var value = (string)GetDatabase().HashGet(key, hashField);

            if (string.IsNullOrEmpty(value))
                return default;

            return JsonSerializer.Deserialize<T>(value);
        }

        public T StringGet<T>(string redisKey, RedisPath nameKey)
        {
            return Deserialize<T>(GetDatabase().StringGet($"{nameKey.Value}:{redisKey}"));
        }

        private string Serialize(object obj)
        {
            if (obj == null)
                return null;

            var result = JsonSerializer.Serialize(obj, StandardResolver.ExcludeNullCamelCase);

            return Encoding.ASCII.GetString(result);
        }

        private T Deserialize<T>(RedisValue stream)
        {
            return !stream.HasValue ? default(T) : JsonSerializer.Deserialize<T>(stream.ToString());
        }
    }
}
