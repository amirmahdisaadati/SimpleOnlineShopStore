using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Infrastructure.Cache
{
    public interface ICacheProvider
    {
        Task<T> Get<T>(string key);
        Task<bool> Set<T>(string key, T value, TimeSpan expiry);
    }
}
