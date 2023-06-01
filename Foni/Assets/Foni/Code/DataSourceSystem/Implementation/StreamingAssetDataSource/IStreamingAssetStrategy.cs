using System.Threading.Tasks;

namespace Foni.Code.DataSourceSystem.Implementation.StreamingAssetDataSource
{
    internal interface IStreamingAssetStrategy
    {
        Task<string> Get(string path);
    }
}