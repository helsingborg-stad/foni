using System;
using System.Collections;
using System.Threading.Tasks;
using Foni.Code.Core;
using UnityEngine;

namespace Foni.Code.SystemPropertiesSystem
{
    public static class GitProperties
    {
        private static IEnumerator LoadCommitTextAsset(Action<string> callback)
        {
            yield return new WaitForSeconds(1.0f);
            var request = Resources.LoadAsync<TextAsset>("_gitcommit");

            yield return request;

            var textAsset = request.asset as TextAsset;

            if (!textAsset)
            {
                callback(null);
                yield break;
            }

            callback(textAsset.text.Length > 7 ? textAsset.text[..7] : textAsset.text);
        }

        public static async Task<string> GetCommitHash()
        {
            string content = null;

            await Globals.ServiceLocator.AsyncService.RunOnMainThread(() => LoadCommitTextAsset((textContent) =>
                content = textContent));

            return content;
        }
    }
}