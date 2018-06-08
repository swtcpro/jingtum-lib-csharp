using System;
using System.Collections.Generic;
using System.Linq;
#if NETSTANDARD
using Microsoft.Extensions.Caching.Memory;
#else
using System.Runtime.Caching;
#endif
using System.Text;
using System.Threading.Tasks;

namespace JingTum.Lib
{
    internal class Cache<T>
    {
        private MemoryCache _cache;
        private readonly int _maxAage;

        public Cache(string name, int maxAge)
        {
#if NETSTANDARD
            _cache = new MemoryCache(new MemoryCacheOptions());
#else
            _cache = new MemoryCache(name);
#endif
            _maxAage = maxAge;
        }

        public void Add(string key, T value)
        {
#if NETSTANDARD
            _cache.Set(key, value, new TimeSpan(TimeSpan.TicksPerMillisecond * _maxAage));
#else
            var policy = new CacheItemPolicy
            {
                AbsoluteExpiration = DateTime.Now.AddMilliseconds(_maxAage)
            };
            _cache.Set(key, value, policy);
#endif
        }

        public T Get(string key)
        {
#if NETSTANDARD
            T result = default(T);
            _cache.TryGetValue<T>(key, out result);
            return result;
#else
            if (!_cache.Contains(key)) return default(T);

            return (T)_cache[key];
#endif
        }

        public object Remove(string key)
        {
#if NETSTANDARD
            object result;
            if(_cache.TryGetValue(key, out result))
            {
                _cache.Remove(key);
                return result;
            }
            return null;
#else
            return _cache.Remove(key);
#endif
        }

        public bool Contains(string key)
        {
#if NETSTANDARD
            object result;
            return _cache.TryGetValue(key, out result);
#else
            return _cache.Contains(key);
#endif
        }
    }
}
