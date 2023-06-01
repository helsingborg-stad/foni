using System;
using System.Collections;
using System.IO;
using System.Threading.Tasks;
using Foni.Code.Core;
using UnityEngine;
using UnityEngine.Networking;

namespace Foni.Code.DataSourceSystem.Implementation.StreamingAssetDataSource
{
    public class UriStreamingAssetsStrategy : IStreamingAssetStrategy
    {
        private static IEnumerator PerformWebRequest(string uri, Action<string> resultCallback)
        {
            var request = UnityWebRequest
                .Get(uri);

            yield return request.SendWebRequest();

            if (request is not { result: UnityWebRequest.Result.Success })
            {
                throw new Exception($"Failed to access uri {uri} ({request.result})");
            }

            resultCallback(request.downloadHandler.text);
        }

        public async Task<string> Get(string path)
        {
            var fileUri = Path.Combine(Application.streamingAssetsPath, path);

            string requestResult = null;

            await Globals.ServiceLocator.AsyncService.RunOnMainThread(() => PerformWebRequest(fileUri,
                result => requestResult = result));

            return requestResult;
        }
    }
}