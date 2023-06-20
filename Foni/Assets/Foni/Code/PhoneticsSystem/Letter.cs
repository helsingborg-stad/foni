using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Foni.Code.DataSourceSystem;
using UnityEngine;

namespace Foni.Code.PhoneticsSystem
{
    public struct Letter
    {
        public string ID;
        public List<Word> Words;
    }

    public static class LetterSerialization
    {
        [Serializable]
        private record SerializedLetter
        {
            public string id;
            public string[] words;
        }

        [Serializable]
        private record SerializedLetterRoot
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
            return new Letter { ID = serializedLetter.id, Words = letterWords };
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