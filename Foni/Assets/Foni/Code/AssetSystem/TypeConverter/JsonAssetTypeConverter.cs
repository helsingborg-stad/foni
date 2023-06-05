using UnityEngine;

namespace Foni.Code.AssetSystem.TypeConverter
{
    public class JsonAssetTypeConverter<TResultType> : IAssetTypeConverter<TResultType>
    {
        public TResultType Convert(string rawAssetContent)
        {
            return JsonUtility.FromJson<TResultType>(rawAssetContent);
        }
    }
}