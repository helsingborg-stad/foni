using System;
using System.Collections.Generic;

namespace Foni.Code.ProfileSystem
{
    [Serializable]
    public struct SingleGuessData
    {
        public string letter;
        public int wrongGuesses;
        public float durationUntilCorrectS;
        public int timesSoundPlayed;
    }

    [Serializable]
    public struct SessionData
    {
        public string timestampStart;
        public float totalSessionTimeS;
        public List<SingleGuessData> guesses;
    }
}