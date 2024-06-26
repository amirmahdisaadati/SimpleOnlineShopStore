using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using RedLockNet.SERedis;
using RedLockNet.SERedis.Configuration;
using StackExchange.Redis;

namespace OnlineShopStore.Infrastructure.Cache
{
    public class RedisCacheProvider : ICacheProvider
    {
        private readonly RedLockFactory _redLockFactory;
        private readonly IDatabase _database;
        private readonly string _connection;

        public RedisCacheProvider(IOptions<RedisConfig> redisConfig)
        {
            _connection = redisConfig.Value.RedisConnection;
            var connectionMult = ConnectionMultiplexer.Connect(_connection);
            _database = connectionMult.GetDatabase();

            var redisEndpoints = new List<RedLockMultiplexer>
            {
                new RedLockMultiplexer(connectionMult)
            };

            _redLockFactory = RedLockFactory.Create(redisEndpoints);
        }

        public async Task<T> Get<T>(string key)
        {
            var lockObject = _redLockFactory.CreateLock(key, TimeSpan.FromSeconds(10));
            using (lockObject)
            {
                if (lockObject.IsAcquired && await _database.KeyExistsAsync(key))
                {
                    var value = await _database.StringGetAsync(key);
                    return JsonSerializer.Deserialize<T>(value, new JsonSerializerOptions
                    {
                        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
                    });
                }
                else
                {
                    return default(T);
                }
            }
        }

        public async Task<bool> Set<T>(string key, T value, TimeSpan expiry)
        {
            var lockObject = _redLockFactory.CreateLock(key, TimeSpan.FromSeconds(10));
            using (lockObject)
            {
                try
                {
                    if (lockObject.IsAcquired)
                    {
                        var serializedValue = JsonSerializer.Serialize(value);
                        var result = await _database.StringSetAsync(key, serializedValue, expiry);
                        return result;
                    }
                    else
                    {
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    return false;
                }

            }
        }
    }
}
