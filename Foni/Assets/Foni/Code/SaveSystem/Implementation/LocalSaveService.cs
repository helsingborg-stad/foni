using System.IO;
using System.Text;
using System.Threading.Tasks;
using Foni.Code.Util;
using UnityEngine;

namespace Foni.Code.SaveSystem.Implementation
{
    public class LocalSaveService : ISaveService
    {
        public async Task<string> Load(string id)
        {
            var dataPath = await PathUtils.GetPersistentDataPath();
            return await File.ReadAllTextAsync(Path.Join(dataPath, id), Encoding.UTF8);
        }

        public async Task Save(string id, string content)
        {
            var dataPath = await PathUtils.GetPersistentDataPath();
            var savePath = Path.Join(dataPath, id);
            Debug.LogFormat("Saving {0}: {1}", savePath, content);
            await File.WriteAllTextAsync(savePath, content, Encoding.UTF8);
        }
    }
}