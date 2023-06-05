using System.Collections.Generic;
using Foni.Code.ServicesSystem;
using UnityEngine;

namespace Foni.Code.AssetSystem
{
    public class AssetCache : IAssetCache, IService
    {
        private readonly Dictionary<string, Object> _cache = new();

        public bool IsCached(string assetPath)
        {
            return _cache.ContainsKey(assetPath);
        }

        public T Get<T>(string assetPath) where T : Object
        {
            return (T)_cache[assetPath];
        }

        public void Update(string assetPath, Object newValue)
        {
            _cache[assetPath] = newValue;
        }
    }
}