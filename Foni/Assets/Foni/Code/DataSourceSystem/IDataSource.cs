using System.Threading.Tasks;

namespace Foni.Code.DataSourceSystem
{
    public interface IDataSource
    {
        Task<T> Load<T>(string path);
        Task<string> LoadRaw(string path);
        Task Save(string path, string rawData);
    }
}