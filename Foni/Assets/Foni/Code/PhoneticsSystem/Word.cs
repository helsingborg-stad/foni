using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Foni.Code.AssetSystem;
using Foni.Code.DataSourceSystem;
using UnityEngine;

namespace Foni.Code.PhoneticsSystem
{
    public struct Word
    {
        public string ID;
        public SoftRef<Sprite> Sprite;
        public SoftRef<AudioClip> SoundClip;

        public bool Equals(Word other)
        {
            return ID == other.ID;
        }

        public override bool Equals(object obj)
        {
            return obj is Word other && Equals(other);
        }

        public override int GetHashCode()
        {
            return (ID != null ? ID.GetHashCode() : 0);
        }
    }

    public static class WordSerialization
    {
        [Serializable]
        public record SerializedWord
        {
            public string id;
            public string image;
            public string soundClip;
        }

        [Serializable]
        public record SerializedWordRoot
        {
            public SerializedWord[] words;
        }

        private static Word Deserialize(SerializedWord serializedWord)
        {
            return new Word
            {
                ID = serializedWord.id,
                Sprite = new SoftRef<Sprite>(serializedWord.image),
                SoundClip = new SoftRef<AudioClip>(serializedWord.soundClip)
            };
        }

        public static async Task<List<Word>> LoadFromDataSource(IDataSource dataSource)
        {
            var rawBase64 = await dataSource.LoadBase64("words.json");
            var raw = Encoding.UTF8.GetString(Convert.FromBase64String(rawBase64));
            return
                JsonUtility.FromJson<SerializedWordRoot>(raw)
                    .words
                    .ToList()
                    .ConvertAll(Deserialize);
        }
    }
}