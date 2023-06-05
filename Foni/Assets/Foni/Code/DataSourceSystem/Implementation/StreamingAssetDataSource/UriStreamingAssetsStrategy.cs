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
        private static IEnumerator PerformWebRequest(string uri, Action<byte[]> resultCallback)
        {
            var request = UnityWebRequest
                .Get(uri);

            yield return request.SendWebRequest();

            if (request is not { result: UnityWebRequest.Result.Success })
            {
                throw new Exception($"Failed to access uri {uri} ({request.result})");
            }

            resultCallback(request.downloadHandler.data);
        }

        public async Task<string> Get(string path)
        {
            var fileUri = Path.Combine(Application.streamingAssetsPath, path);

            byte[] bytes = { };

            await Globals.ServiceLocator.AsyncService.RunOnMainThread(() => PerformWebRequest(fileUri,
                result => bytes = result));

            return Convert.ToBase64String(bytes);
        }
    }
}