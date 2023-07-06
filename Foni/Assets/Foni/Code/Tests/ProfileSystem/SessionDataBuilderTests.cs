using System;
using Foni.Code.Core;
using Foni.Code.DateTimeSystem;
using Foni.Code.ProfileSystem;
using Foni.Code.ServicesSystem;
using NUnit.Framework;
using TimeSpan = System.TimeSpan;

namespace Foni.Code.Tests.ProfileSystem
{
    public class SessionDataBuilderTests
    {
        private class MockDateTimeService : IDateTimeService
        {
            public DateTime Now { get; set; }

            public static MockDateTimeService Get()
            {
                var dateTimeService = new MockDateTimeService();
                Globals.ServiceLocator = new CoreServiceLocator();
                Globals.ServiceLocator.Register(EService.DateTimeService, dateTimeService);
                dateTimeService.Now = new DateTime(2023, 06, 30, 15, 30, 45, DateTimeKind.Utc);
                return dateTimeService;
            }
        }

        [Test]
        public void Initialize()
        {
            var dateTimeService = MockDateTimeService.Get();
            var builder = new SessionDataBuilder();
            builder.Initialize();

            dateTimeService.Now += new TimeSpan(0, 2, 0);
            var sessionData = builder.EndSession();

            Assert.AreEqual("2023-06-30T15:30:45.0000000Z", sessionData.timestampStart);
            Assert.AreEqual(120, sessionData.totalSessionTimeS);
        }

        [Test]
        public void StartGuess()
        {
            var dateTimeService = MockDateTimeService.Get();
            var builder = new SessionDataBuilder()
                .Initialize()
                .StartGuess("a");

            dateTimeService.Now += new TimeSpan(0, 1, 0);
            var guesses = builder
                .EndGuess()
                .EndSession()
                .guesses;

            Assert.AreEqual(1, guesses.Count);
            Assert.AreEqual("a", guesses[0].letter);
            Assert.AreEqual(60, guesses[0].durationUntilCorrectS);
        }

        [Test]
        public void IncrementWrongGuesses()
        {
            var dateTimeService = MockDateTimeService.Get();
            var builder = new SessionDataBuilder()
                .Initialize()
                .StartGuess("a");

            dateTimeService.Now += new TimeSpan(0, 1, 0);
            var guesses = builder
                .EndGuess()
                .IncrementWrongGuesses()
                .IncrementWrongGuesses()
                .EndSession()
                .guesses;

            Assert.AreEqual(2, guesses[0].wrongGuesses);
        }

        [Test]
        public void IncrementTimesSoundPlayed()
        {
            var dateTimeService = MockDateTimeService.Get();
            var builder = new SessionDataBuilder()
                .Initialize()
                .StartGuess("a");

            dateTimeService.Now += new TimeSpan(0, 1, 0);
            var guesses = builder
                .EndGuess()
                .IncrementTimesSoundPlayed()
                .IncrementTimesSoundPlayed()
                .IncrementTimesSoundPlayed()
                .EndSession()
                .guesses;

            Assert.AreEqual(3, guesses[0].timesSoundPlayed);
        }
    }
}