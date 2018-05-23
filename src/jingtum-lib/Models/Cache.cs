using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
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
            _cache = new MemoryCache(name);
            _maxAage = maxAge;
        }

        public void Add(string key, T value)
        {
            var policy = new CacheItemPolicy
            {
                AbsoluteExpiration = DateTime.Now.AddMilliseconds(_maxAage)
            };
            _cache.Set(key, value, policy);
        }

        public T Get(string key)
        {
            if (!_cache.Contains(key)) return default(T);

            return (T)_cache[key];
        }

        public void Remove(string key)
        {
            _cache.Remove(key);
        }

        public bool Contains(string key)
        {
            return _cache.Contains(key);
        }
    }
}
