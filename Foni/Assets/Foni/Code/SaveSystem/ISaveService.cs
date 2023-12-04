using System.Threading.Tasks;
using Foni.Code.ServicesSystem;

namespace Foni.Code.SaveSystem
{
    public interface ISaveService : IService
    {
        public Task<string> Load(string id);
        public Task<string> LoadOrDefault(string id, string defaultValue);
        public Task Save(string id, string content);
    }
}