using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RedLockNet.SERedis;
using StackExchange.Redis;

namespace OnlineShopStore.Infrastructure.Cache
{
    public class RedisCacheProvider : ICacheProvider
    {
        private readonly RedLockFactory _redLockFactory;
        private readonly IDatabase _database;

        public RedisCacheProvider()
        {
            var connectionMult = ConnectionMultiplexer.Connect("");
            _database = connectionMult.GetDatabase();

        }
        public Task<T> Get<T>(string key)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Set<T>(string key, T value, TimeSpan expiry)
        {
            throw new NotImplementedException();
        }
    }
}
