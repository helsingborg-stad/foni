using UnityEngine;

namespace Foni.Code.AssetSystem.TypeConverter
{
    public class AudioClipAssetTypeConverter : IAssetTypeConverter<AudioClip>
    {
        public AudioClip Convert(string rawAssetContent)
        {
            var rawBytes = System.Convert.FromBase64String(rawAssetContent);
            return OpenWavParser.ByteArrayToAudioClip(rawBytes, "loadedClip");
        }
    }
}