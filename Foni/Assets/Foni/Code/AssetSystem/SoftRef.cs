using System.Collections;
using System.Threading.Tasks;
using Foni.Code.AssetSystem.TypeConverter;
using Foni.Code.Core;
using Foni.Code.DataSourceSystem;
using UnityEngine;

namespace Foni.Code.AssetSystem
{
    public class SoftRef<TAssetType> where TAssetType : Object
    {
        private readonly string _assetPath;

        public SoftRef(string assetPath)
        {
            _assetPath = assetPath;
        }

        public TAssetType Asset { get; private set; }

        public async Task Load(IDataSource dataSource)
        {
            if (IsValid())
            {
                return;
            }

            var cache = Globals.ServiceLocator.AssetCache;
            if (cache.IsCached(_assetPath))
            {
                Debug.LogFormat("Using cache for {0}", _assetPath);
                Asset = cache.Get<TAssetType>(_assetPath);
                return;
            }

            var rawAssetContent = await dataSource.LoadBase64(_assetPath);
            Debug.LogFormat("Loading {0}", _assetPath);
            await Globals.ServiceLocator.AsyncService.RunOnMainThread(() => ConvertRaw(rawAssetContent));
        }

        public bool IsValid()
        {
            return Asset != null;
        }

        private IEnumerator ConvertRaw(string rawAssetContent)
        {
            Asset = new AssetTypeConverterFactory<TAssetType>().GetConverter().Convert(rawAssetContent);

            var cache = Globals.ServiceLocator.AssetCache;
            cache.Update(_assetPath, Asset);
            yield break;
        }
    }
}