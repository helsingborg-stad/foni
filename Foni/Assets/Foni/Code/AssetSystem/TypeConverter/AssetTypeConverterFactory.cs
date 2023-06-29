using System;
using System.Collections.Generic;
using UnityEngine;

namespace Foni.Code.AssetSystem.TypeConverter
{
    public class AssetTypeConverterFactory<TResultType>
    {
        private readonly Dictionary<Type, IAssetTypeConverter<TResultType>> _converterMap = new()
        {
            { typeof(Sprite), new SpriteAssetTypeConverter() as IAssetTypeConverter<TResultType> },
            { typeof(AudioClip), new AudioClipAssetTypeConverter() as IAssetTypeConverter<TResultType> }
        };

        public IAssetTypeConverter<TResultType> GetConverter()
        {
            var assetType = typeof(TResultType);

            if (_converterMap.TryGetValue(assetType, out var converter))
            {
                return converter;
            }

            Debug.LogWarningFormat("No asset type converter for type {0} found - using JSON fallback", assetType);
            return new JsonAssetTypeConverter<TResultType>();
        }
    }
}