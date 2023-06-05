using UnityEngine;

namespace Foni.Code.AssetSystem.TypeConverter
{
    public class SpriteAssetTypeConverter : IAssetTypeConverter<Sprite>
    {
        public Sprite Convert(string rawAssetContent)
        {
            var rawBytes = System.Convert.FromBase64String(rawAssetContent);
            var texture = new Texture2D(2, 2);
            texture.LoadImage(rawBytes);
            var sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.one * 0.5f);
            return sprite;
        }
    }
}