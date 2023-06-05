using UnityEngine;

namespace Foni.Code.AssetSystem
{
    public interface IAssetCache
    {
        public bool IsCached(string assetPath);
        public T Get<T>(string assetPath) where T : Object;
        public void Update(string assetPath, Object newValue);
    }
}