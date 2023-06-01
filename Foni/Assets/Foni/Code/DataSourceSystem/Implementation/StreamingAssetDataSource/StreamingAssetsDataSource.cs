using System;
using System.Threading.Tasks;
using UnityEngine;

namespace Foni.Code.DataSourceSystem.Implementation.StreamingAssetDataSource
{
    public class StreamingAssetsDataSource : IDataSource
    {
        private static IStreamingAssetStrategy Strategy
        {
            get
            {
#if UNITY_ANDROID
                return new UriStreamingAssetsStrategy();
#endif
                return new FileAccessStreamingAssetsStrategy();
            }
        }

        public async Task<T> Load<T>(string path)
        {
            var raw = await LoadRaw(path);
            return JsonUtility.FromJson<T>(raw);
        }

        public async Task<string> LoadRaw(string path)
        {
            var result = await Strategy.Get(path);
            return result;
        }

        public Task Save(string path, string rawData)
        {
            throw new NotImplementedException();
        }
    }
}