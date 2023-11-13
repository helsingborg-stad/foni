using System;
using System.Collections.Generic;
using System.Linq;
using Foni.Code.Core;

namespace Foni.Code.ProfileSystem
{
    public class SessionDataBuilder
    {
        private SessionData _sessionData;
        private int _currentGuessIndex;
        private DateTime _roundStartTimestamp;
        private DateTime _guessStartTimestamp;

        public SessionDataBuilder Initialize()
        {
            _sessionData.timestampStart = Globals.ServiceLocator.DateTimeService.Now.ToString("O");
            _sessionData.guesses = new List<SingleGuessData>();
            _roundStartTimestamp = Globals.ServiceLocator.DateTimeService.Now;
            return this;
        }

        public SessionDataBuilder StartGuess(string letter)
        {
            _currentGuessIndex = _sessionData.guesses.Count;
            _guessStartTimestamp = Globals.ServiceLocator.DateTimeService.Now;

            _sessionData.guesses.Add(new SingleGuessData
            {
                letter = letter,
                wrongGuesses = 0,
                timesSoundPlayed = 0,
                durationUntilCorrectS = -1
            });
            return this;
        }

        public SessionDataBuilder IncrementWrongGuesses()
        {
            var singleGuessData = _sessionData.guesses[_currentGuessIndex];
            singleGuessData.wrongGuesses += 1;
            _sessionData.guesses[_currentGuessIndex] = singleGuessData;
            return this;
        }

        public SessionDataBuilder IncrementTimesSoundPlayed()
        {
            var singleGuessData = _sessionData.guesses[_currentGuessIndex];
            singleGuessData.timesSoundPlayed += 1;
            _sessionData.guesses[_currentGuessIndex] = singleGuessData;
            return this;
        }

        public SessionDataBuilder EndGuess()
        {
            var endTimeSpan = Globals.ServiceLocator.DateTimeService.Now - _guessStartTimestamp;
            var singleGuessData = _sessionData.guesses[_currentGuessIndex];
            singleGuessData.durationUntilCorrectS = (float)endTimeSpan.TotalSeconds;
            _sessionData.guesses[_currentGuessIndex] = singleGuessData;
            return this;
        }

        public SessionDataBuilder FlagHelpUsed()
        {
            var singleGuessData = _sessionData.guesses[_currentGuessIndex];
            singleGuessData.usedHelp = true;
            _sessionData.guesses[_currentGuessIndex] = singleGuessData;
            return this;
        }

        public SessionData EndSession()
        {
            var sessionTimeSpan = Globals.ServiceLocator.DateTimeService.Now - _roundStartTimestamp;
            _sessionData.totalSessionTimeS = (float)sessionTimeSpan.TotalSeconds;
            _sessionData.guesses = _sessionData.guesses.Where(guess => guess.durationUntilCorrectS >= 0).ToList();
            return _sessionData;
        }
    }
}