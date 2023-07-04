using System;
using System.Collections.Generic;
using Foni.Code.Core;

namespace Foni.Code.ProfileSystem
{
    public class RoundDataBuilder
    {
        private RoundData _roundData;
        private int _currentGuessIndex;
        private DateTime _roundStartTimestamp;
        private DateTime _guessStartTimestamp;

        public RoundDataBuilder Initialize()
        {
            _roundData.timestampStart = Globals.ServiceLocator.DateTimeService.Now.ToString("O");
            _roundData.guesses = new List<SingleGuessData>();
            _roundStartTimestamp = Globals.ServiceLocator.DateTimeService.Now;

            return this;
        }

        public RoundDataBuilder StartGuess(string letter)
        {
            _currentGuessIndex = _roundData.guesses.Count;
            _guessStartTimestamp = Globals.ServiceLocator.DateTimeService.Now;

            _roundData.guesses.Add(new SingleGuessData
            {
                letter = letter,
                wrongGuesses = 0,
                timesSoundPlayed = 0,
                durationUntilCorrectS = -1
            });

            return this;
        }

        public RoundDataBuilder IncrementWrongGuesses()
        {
            var singleGuessData = _roundData.guesses[_currentGuessIndex];
            singleGuessData.wrongGuesses += 1;
            _roundData.guesses[_currentGuessIndex] = singleGuessData;
            return this;
        }

        public RoundDataBuilder IncrementTimesSoundPlayed()
        {
            var singleGuessData = _roundData.guesses[_currentGuessIndex];
            singleGuessData.timesSoundPlayed += 1;
            _roundData.guesses[_currentGuessIndex] = singleGuessData;
            return this;
        }

        public RoundDataBuilder EndGuess()
        {
            var endTimeSpan = Globals.ServiceLocator.DateTimeService.Now - _guessStartTimestamp;
            var singleGuessData = _roundData.guesses[_currentGuessIndex];
            singleGuessData.durationUntilCorrectS = (float)endTimeSpan.TotalSeconds;
            _roundData.guesses[_currentGuessIndex] = singleGuessData;
            return this;
        }

        public RoundData EndRound()
        {
            var sessionTimeSpan = Globals.ServiceLocator.DateTimeService.Now - _roundStartTimestamp;
            _roundData.totalSessionTimeS = (float)sessionTimeSpan.TotalSeconds;
            return _roundData;
        }
    }
}