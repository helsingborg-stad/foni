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
    }

    public static class WordSerialization
    {
        [Serializable]
        private record SerializedWord
        {
            public string id;
            public string image;
        }

        [Serializable]
        private record SerializedWordRoot
        {
            public SerializedWord[] words;
        }

        private static Word Deserialize(SerializedWord serializedWord)
        {
            return new Word
            {
                ID = serializedWord.id,
                Sprite = new SoftRef<Sprite>(serializedWord.image)
            };
        }

        public static async Task<List<Word>> LoadFromDataSource(IDataSource dataSource)
        {
            var rawBase64 = await dataSource.LoadBase64("words.json");
            var raw = Encoding.Default.GetString(Convert.FromBase64String(rawBase64));
            return
                JsonUtility.FromJson<SerializedWordRoot>(raw)
                    .words
                    .ToList()
                    .ConvertAll(Deserialize);
        }
    }
}