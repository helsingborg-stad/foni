using System;
using System.Collections.Generic;
using Foni.Code.ProfileSystem;
using NUnit.Framework;

namespace Foni.Code.Tests.ProfileSystem
{
    public class ProfileSystemTests
    {
        private static ProfileData CreateTestProfile()
        {
            var profile = new ProfileData
            {
                name = "Räv",
                icon = "fox",
                statistics = new StatisticsData
                {
                    sessions = new List<SessionData>
                    {
                        new()
                        {
                            timestampStart = new DateTime(2023, 06, 30, 15, 30, 45, DateTimeKind.Utc).ToString("O"),
                            totalSessionTimeS = (float)new TimeSpan(0, 3, 15).TotalSeconds,
                            guesses = new List<SingleGuessData>
                            {
                                new()
                                {
                                    letter = "s",
                                    wrongGuesses = 3,
                                    durationUntilCorrectS = (float)new TimeSpan(0, 0, 30).TotalSeconds,
                                    timesSoundPlayed = 1,
                                    usedHelp = true
                                }
                            }
                        }
                    }
                }
            };

            profile.statistics.sessions[0].guesses.Add(new SingleGuessData
            {
                letter = "i",
                wrongGuesses = 0,
                durationUntilCorrectS = (float)new TimeSpan(0, 0, 2).TotalSeconds,
                timesSoundPlayed = 0,
                usedHelp = false
            });

            return profile;
        }

        private static string GetTestProfileJson()
        {
            return
                "{\"name\":\"Räv\",\"icon\":\"fox\",\"statistics\":{\"sessions\":[{\"timestampStart\":\"2023-06-30T15:30:45.0000000Z\",\"totalSessionTimeS\":195.0,\"guesses\":[{\"letter\":\"s\",\"wrongGuesses\":3,\"durationUntilCorrectS\":30.0,\"timesSoundPlayed\":1,\"usedHelp\":true},{\"letter\":\"i\",\"wrongGuesses\":0,\"durationUntilCorrectS\":2.0,\"timesSoundPlayed\":0,\"usedHelp\":false}]}]}}";
        }

        [Test]
        public void SerializeProfile()
        {
            var serialized = CreateTestProfile().Serialize();

            Assert.AreEqual(GetTestProfileJson(), serialized);
        }

        [Test]
        public void DeserializeProfile()
        {
            var deserialized = ProfileData.Deserialize(GetTestProfileJson());

            Assert.AreEqual(CreateTestProfile().Serialize(), deserialized.Serialize());
        }
    }
}