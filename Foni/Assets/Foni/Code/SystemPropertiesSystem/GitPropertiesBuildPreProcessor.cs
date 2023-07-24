#if UNITY_EDITOR
using System.Diagnostics;
using System.IO;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace Foni.Code.SystemPropertiesSystem
{
    public class GitPropertiesBuildPreProcessor : IPreprocessBuildWithReport
    {
        public int callbackOrder => 0;

        public void OnPreprocessBuild(BuildReport report)
        {
            Debug.Log("Running Git properties preprocessor");
            if (IsGitInstalled())
            {
                var outPath = Path.Join(Application.dataPath, "Foni/Resources/_gitcommit.txt");
                var commitHash = GetGitCommitHash();
                File.WriteAllText(outPath, commitHash);
            }
            else
            {
                Debug.LogWarning("Git is not found - version string will be inaccurate!");
            }
        }

        private static bool IsGitInstalled()
        {
            try
            {
                var startInfo = new ProcessStartInfo("git", "--version")
                    { RedirectStandardOutput = true, UseShellExecute = false };
                var process = Process.Start(startInfo);

                if (process != null)
                {
                    process.WaitForExit();
                    return true;
                }
            }
            catch
            {
                // ignored
            }

            return false;
        }

        private static string GetGitCommitHash()
        {
            var startInfo = new ProcessStartInfo("git", "rev-parse HEAD")
            {
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            using var process = new Process();
            process.StartInfo = startInfo;
            process.Start();
            var output = process.StandardOutput.ReadToEnd();
            process.WaitForExit();
            return output.Trim();
        }
    }
}
#endif