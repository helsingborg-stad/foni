using System;
using System.Collections.Generic;
using UnityEngine;

namespace Foni.Code.AssetSystem.TypeConverter
{
    public class AssetTypeConverterFactory<TResultType>
    {
        private readonly Dictionary<Type, IAssetTypeConverter<TResultType>> _converterMap = new()
        {
            { typeof(Sprite), new SpriteAssetTypeConverter() as IAssetTypeConverter<TResultType> }
        };

        public IAssetTypeConverter<TResultType> GetConverter()
        {
            var assetType = typeof(TResultType);

            return _converterMap.TryGetValue(assetType, out var converter)
                ? converter
                : new JsonAssetTypeConverter<TResultType>();
        }
    }
}