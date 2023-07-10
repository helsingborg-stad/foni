using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
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

            var letters =
                JsonUtility.FromJson<LetterSerialization.SerializedLetterRoot>(
                        File.ReadAllText(Path.Join(assetsPath, "letters.json"), Encoding.UTF8))
                    .letters
                    .ToList();

            var wordList = JsonUtility.FromJson<WordSerialization.SerializedWordRoot>(
                    File.ReadAllText(Path.Join(assetsPath, "words.json"), Encoding.UTF8))
                .words
                .ToList();

            letters.ForEach(letter => LogVerifyLetter(letter, wordList));
            wordList.ForEach(LogVerifyWord);

            var assetFiles = Directory.GetFiles(Application.streamingAssetsPath, "*", SearchOption.AllDirectories)
                .Where(file => file.EndsWith(".png") || file.EndsWith(".wav"))
                .ToList()
                .ConvertAll(fullPath => Path.GetRelativePath(Application.streamingAssetsPath, fullPath));
            assetFiles.ForEach(path => LogPossiblyUnusedAsset(path, letters, wordList));

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

        private static void LogPossiblyUnusedAsset(string partialAssetPath,
            IEnumerable<LetterSerialization.SerializedLetter> letters,
            IEnumerable<WordSerialization.SerializedWord> words)
        {
            var isInLetter = letters.Any(letter => letter.handGestureImage == partialAssetPath);
            var isInWord = words.Any(word => word.image == partialAssetPath || word.soundClip == partialAssetPath);

            if (!isInLetter && !isInWord)
            {
                Debug.LogWarningFormat("Asset '{0}' not used by any letter/word", partialAssetPath);
            }
        }
    }
}