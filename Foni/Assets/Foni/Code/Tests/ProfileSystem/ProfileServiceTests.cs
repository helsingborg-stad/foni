using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Foni.Code.ProfileSystem;
using Foni.Code.SaveSystem;
using NUnit.Framework;

namespace Foni.Code.Tests.ProfileSystem
{
    public class ProfileServiceTests
    {
        private static readonly ProfileData MockProfile = new()
        {
            name = "Räv",
            icon = "fox",
            statistics = new StatisticsData
            {
                rounds = new RoundData[]
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
                                timesSoundPlayed = 1
                            },
                            new()
                            {
                                letter = "i",
                                wrongGuesses = 0,
                                durationUntilCorrectS = (float)new TimeSpan(0, 0, 2).TotalSeconds,
                                timesSoundPlayed = 0
                            }
                        }
                    }
                }
            }
        };

        private static string GetMockProfileJson()
        {
            return MockProfile.Serialize();
        }

        private class MockSaveService : ISaveService
        {
            public Task<string> Load(string id)
            {
                return Task.FromResult("{\"profiles\": [" + GetMockProfileJson() + "]}");
            }

            public Task Save(string id, string content)
            {
                return Task.CompletedTask;
            }
        }

        [Test]
        public async Task GetAllProfiles()
        {
            var profileService = new ProfileService(new MockSaveService());

            var profiles = await profileService.GetAllProfiles();

            Assert.AreEqual(1, profiles.Count);
            Assert.AreEqual(profiles[0].Serialize(), GetMockProfileJson());
        }
    }
}