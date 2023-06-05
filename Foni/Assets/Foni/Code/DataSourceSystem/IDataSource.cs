using System.Threading.Tasks;

namespace Foni.Code.DataSourceSystem
{
    public interface IDataSource
    {
        Task<string> LoadBase64(string path);
    }
}