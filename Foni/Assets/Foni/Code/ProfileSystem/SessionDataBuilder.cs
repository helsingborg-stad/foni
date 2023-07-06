using System;
using System.Collections.Generic;
using Foni.Code.Core;
using UnityEngine;

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

            Debug.LogFormat("[SessionData] Initialize {0}", _sessionData.timestampStart);
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

            Debug.LogFormat("[SessionData] StartGuess {0:O}", _guessStartTimestamp);
            return this;
        }

        public SessionDataBuilder IncrementWrongGuesses()
        {
            var singleGuessData = _sessionData.guesses[_currentGuessIndex];
            singleGuessData.wrongGuesses += 1;
            _sessionData.guesses[_currentGuessIndex] = singleGuessData;
            
            Debug.LogFormat("[SessionData] IncrementWrongGuesses {0}", singleGuessData.wrongGuesses);
            return this;
        }

        public SessionDataBuilder IncrementTimesSoundPlayed()
        {
            var singleGuessData = _sessionData.guesses[_currentGuessIndex];
            singleGuessData.timesSoundPlayed += 1;
            _sessionData.guesses[_currentGuessIndex] = singleGuessData;
            
            Debug.LogFormat("[SessionData] IncrementTimesSoundPlayed {0}", singleGuessData.timesSoundPlayed);
            return this;
        }

        public SessionDataBuilder EndGuess()
        {
            var endTimeSpan = Globals.ServiceLocator.DateTimeService.Now - _guessStartTimestamp;
            var singleGuessData = _sessionData.guesses[_currentGuessIndex];
            singleGuessData.durationUntilCorrectS = (float)endTimeSpan.TotalSeconds;
            _sessionData.guesses[_currentGuessIndex] = singleGuessData;

            Debug.LogFormat("[SessionData] EndGuess {0}s", singleGuessData.durationUntilCorrectS);
            return this;
        }

        public SessionData EndSession()
        {
            var sessionTimeSpan = Globals.ServiceLocator.DateTimeService.Now - _roundStartTimestamp;
            _sessionData.totalSessionTimeS = (float)sessionTimeSpan.TotalSeconds;

            Debug.LogFormat("[SessionData] EndSession {0}s", _sessionData.totalSessionTimeS);
            return _sessionData;
        }
    }
}