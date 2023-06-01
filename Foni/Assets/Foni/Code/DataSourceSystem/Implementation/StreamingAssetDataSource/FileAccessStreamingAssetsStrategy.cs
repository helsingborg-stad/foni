using System.IO;
using System.Threading.Tasks;
using UnityEngine;

namespace Foni.Code.DataSourceSystem.Implementation.StreamingAssetDataSource
{
    public class FileAccessStreamingAssetsStrategy : IStreamingAssetStrategy
    {
        public Task<string> Get(string path)
        {
            var fileUri = Path.Combine(Application.streamingAssetsPath, path);
            return File.ReadAllTextAsync(fileUri);
        }
    }
}