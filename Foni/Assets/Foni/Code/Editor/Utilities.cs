using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Foni.Code.PhoneticsSystem;
using UnityEngine;

namespace Foni.Code.Editor
{
    public static class Utilities
    {
        public static List<LetterSerialization.SerializedLetter> GetLetters()
        {
            return
                JsonUtility.FromJson<LetterSerialization.SerializedLetterRoot>(
                        File.ReadAllText(Path.Join(Application.streamingAssetsPath, "letters.json"), Encoding.UTF8))
                    .letters
                    .ToList();
        }

        public static List<WordSerialization.SerializedWord> GetWords()
        {
            return JsonUtility.FromJson<WordSerialization.SerializedWordRoot>(
                    File.ReadAllText(Path.Join(Application.streamingAssetsPath, "words.json"), Encoding.UTF8))
                .words
                .ToList();
        }

        public static List<string> GetUnusedAssets()
        {
            var letters = GetLetters();
            var words = GetWords();

            return Directory.GetFiles(Application.streamingAssetsPath, "*", SearchOption.AllDirectories)
                .Where(file => file.EndsWith(".png") || file.EndsWith(".wav"))
                .Select(fullPath => Path.GetRelativePath(Application.streamingAssetsPath, fullPath))
                .Where(asset => IsAssetUnused(asset, letters, words))
                .ToList();
        }

        public static bool IsAssetUsed(string partialAssetPath,
            IEnumerable<LetterSerialization.SerializedLetter> letters,
            IEnumerable<WordSerialization.SerializedWord> words)
        {
            return letters.Any(letter =>
                       letter.handGestureImage == partialAssetPath || letter.altImage == partialAssetPath) ||
                   words.Any(word => word.image == partialAssetPath || word.soundClip == partialAssetPath);
        }

        public static bool IsAssetUnused(string partialAssetPath,
            IEnumerable<LetterSerialization.SerializedLetter> letters,
            IEnumerable<WordSerialization.SerializedWord> words)
        {
            return !IsAssetUsed(partialAssetPath, letters, words);
        }
    }
}