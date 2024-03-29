using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Foni.Code.AssetSystem;
using Foni.Code.DataSourceSystem;
using JetBrains.Annotations;
using UnityEngine;

namespace Foni.Code.PhoneticsSystem
{
    public struct Letter
    {
        public string ID;
        public List<Word> Words;
        public SoftRef<Sprite> HandGestureSprite;
        public SoftRef<AudioClip> VocalizationSound;
        [CanBeNull] public SoftRef<Sprite> AltImage;

        private bool Equals(Letter other)
        {
            return ID == other.ID;
        }

        public override bool Equals(object obj)
        {
            return obj is Letter other && Equals(other);
        }

        public override int GetHashCode()
        {
            return ID != null ? ID.GetHashCode() : 0;
        }
    }

    public static class LetterSerialization
    {
        [Serializable]
        public record SerializedLetter
        {
            public string id;
            public string[] words;
            public string handGestureImage;
            public string vocalizationSound;
            [CanBeNull] public string altImage;
        }

        [Serializable]
        public record SerializedLetterRoot
        {
            public SerializedLetter[] letters;
        }

        private static Letter Deserialize(SerializedLetter serializedLetter, List<Word> wordSource)
        {
            Word WordIdToWord(string desiredWord)
            {
                var index = wordSource.FindIndex(possibleMatch => possibleMatch.ID == desiredWord);
                if (index == -1)
                {
                    throw new Exception($"No word '{desiredWord}' in word source!");
                }

                return wordSource[index];
            }

            var letterWords =
                serializedLetter.words
                    .ToList()
                    .ConvertAll(WordIdToWord);
            return new Letter
            {
                ID = serializedLetter.id,
                Words = letterWords,
                HandGestureSprite = new SoftRef<Sprite>(serializedLetter.handGestureImage),
                VocalizationSound = new SoftRef<AudioClip>(serializedLetter.vocalizationSound),
                AltImage = serializedLetter.altImage != null ? new SoftRef<Sprite>(serializedLetter.altImage) : null,
            };
        }

        public static async Task<List<Letter>> LoadFromDataSource(IDataSource dataSource, List<Word> wordSource)
        {
            var rawBase64 = await dataSource.LoadBase64("letters.json");
            var raw = Encoding.UTF8.GetString(Convert.FromBase64String(rawBase64));
            return
                JsonUtility.FromJson<SerializedLetterRoot>(raw)
                    .letters
                    .ToList()
                    .ConvertAll(letter => Deserialize(letter, wordSource));
        }
    }
}