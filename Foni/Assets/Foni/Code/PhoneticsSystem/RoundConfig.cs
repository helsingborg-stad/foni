using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Foni.Code.DataSourceSystem;
using UnityEngine;

namespace Foni.Code.PhoneticsSystem
{
    public struct Round
    {
        public List<Letter> Letters;
    }

    public struct RoundConfig
    {
        public List<Round> Rounds;
    }

    public static class RoundConfigSerialization
    {
        [Serializable]
        private record SerializedRound
        {
            public string[] letters;
        }

        [Serializable]
        private record SerializedRoundConfig
        {
            public SerializedRound[] rounds;
        }

        private static Round DeserializeRound(SerializedRound serializedRound, List<Letter> letterSource)
        {
            Letter LetterIdToLetter(string letterId)
            {
                var index = letterSource.FindIndex(possibleMatch => possibleMatch.ID == letterId);
                if (index == -1)
                {
                    throw new Exception($"No letter '{letterId}' in letter source!");
                }

                return letterSource[index];
            }

            return new Round
            {
                Letters = serializedRound.letters
                    .ToList()
                    .ConvertAll(LetterIdToLetter)
            };
        }

        private static RoundConfig Deserialize(SerializedRoundConfig serializedRoundConfig, List<Letter> letterSource)
        {
            return new RoundConfig
            {
                Rounds = serializedRoundConfig.rounds
                    .ToList()
                    .ConvertAll(round => DeserializeRound(round, letterSource))
            };
        }

        public static async Task<RoundConfig> LoadFromDataSource(IDataSource dataSource, List<Letter> letterSource)
        {
            var rawBase64 = await dataSource.LoadBase64("roundConfig.json");
            var raw = Encoding.UTF8.GetString(Convert.FromBase64String(rawBase64));
            return Deserialize(JsonUtility.FromJson<SerializedRoundConfig>(raw), letterSource);
        }
    }
}