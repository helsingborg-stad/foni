using System.IO;
using System.Threading.Tasks;
using UnityEngine;

namespace Foni.Code.DataSourceSystem.Implementation.StreamingAssetDataSource
{
    public class FileAccessStreamingAssetsStrategy : IStreamingAssetStrategy
    {
        public async Task<string> Get(string path)
        {
            var fileUri = Path.Combine(Application.streamingAssetsPath, path);
            var bytes = await File.ReadAllBytesAsync(fileUri);
            return System.Convert.ToBase64String(bytes);
        }
    }
}