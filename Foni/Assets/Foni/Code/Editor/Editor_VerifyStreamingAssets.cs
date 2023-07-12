using System.Collections.Generic;
using System.IO;
using System.Linq;
using Foni.Code.PhoneticsSystem;
using UnityEditor;
using UnityEngine;

namespace Foni.Code.Editor
{
    public static partial class FoniEditorUtilities
    {
        [MenuItem("Foni/Verify Streaming Assets")]
        public static void VerifyStreamingAssets()
        {
            var assetsPath = Application.streamingAssetsPath;
            Debug.LogFormat("Streaming assets path: {0}", assetsPath);

            var letters = Utilities.GetLetters();
            var words = Utilities.GetWords();

            void CurriedLogVerifyLetter(LetterSerialization.SerializedLetter letter) => LogVerifyLetter(letter, words);

            letters.ForEach(CurriedLogVerifyLetter);
            words.ForEach(LogVerifyWord);

            Utilities.GetUnusedAssets()
                .ForEach(assetPath =>
                    Debug.LogWarningFormat("Asset '{0}' not used by any letter/word", assetPath));

            Debug.Log("Verify done!");
        }

        private static void LogVerifyLetter(LetterSerialization.SerializedLetter letter,
            List<WordSerialization.SerializedWord> wordList)
        {
            var assetsPath = Application.streamingAssetsPath;
            var missingWords = letter.words
                .ToList()
                .FindAll(word => !wordList.Exists(maybeRightWord => word == maybeRightWord.id));

            if (missingWords.Count > 0)
            {
                Debug.LogWarningFormat("Following words are missing for letter '{0}': {1}", letter.id,
                    string.Join(", ", missingWords));
            }

            var missingHandGestureImage = !File.Exists(Path.Join(
                assetsPath,
                letter.handGestureImage));

            if (missingHandGestureImage)
            {
                Debug.LogWarningFormat("Letter '{0}' missing hand gesture image '{1}'", letter.id,
                    letter.handGestureImage);
            }
        }

        private static void LogVerifyWord(WordSerialization.SerializedWord word)
        {
            var assetsPath = Application.streamingAssetsPath;

            var missingImage = !File.Exists(Path.Join(assetsPath, word.image));
            if (missingImage)
            {
                Debug.LogWarningFormat("Word '{0}' missing image '{1}'", word.id,
                    word.image);
            }

            var missingSound = !File.Exists(Path.Join(assetsPath, word.soundClip));
            if (missingSound)
            {
                Debug.LogWarningFormat("Word '{0}' missing sound '{1}'", word.id,
                    word.soundClip);
            }
        }
    }
}