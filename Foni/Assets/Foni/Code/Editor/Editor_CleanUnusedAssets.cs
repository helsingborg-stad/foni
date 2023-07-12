using System.IO;
using UnityEditor;
using UnityEngine;

namespace Foni.Code.Editor
{
#if UNITY_EDITOR
    public static partial class FoniEditorUtilities
    {
        [MenuItem("Foni/Clean Unused Assets")]
        public static void CleanUnusedAssets()
        {
            var unusedAssets = Utilities.GetUnusedAssets();

            if (unusedAssets.Count == 0)
            {
                EditorUtility.DisplayDialog("Unused Assets", "No unused assets found!", "ok");
                return;
            }

            var message = $"Found {unusedAssets.Count} unused asset(s):\n" +
                          string.Join(", ", unusedAssets);

            void RemoveFile(string filePath)
            {
                File.Delete(Path.Join(Application.streamingAssetsPath, filePath));
                Debug.LogFormat("Removed {0}", filePath);
            }

            if (EditorUtility.DisplayDialog("Unused Assets", message, "Clean", "Cancel"))
            {
                unusedAssets.ForEach(RemoveFile);
            }
        }
    }
#endif
}