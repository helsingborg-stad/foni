using System;

namespace Foni.Code.PhoneticsSystem
{
    public static class PhoneticsSerialization
    {
        [Serializable]
        public record SerializedLetter
        {
            public string id;
        }

        [Serializable]
        public record SerializedLetterRoot
        {
            public SerializedLetter[] letters;
        }

        public static Letter DeserializeLetter(SerializedLetter serializedLetter)
        {
            return new Letter { id = serializedLetter.id };
        }

        public static SerializedLetter SerializeLetter(Letter letter)
        {
            return new SerializedLetter { id = letter.id };
        }
    }
}