using System.Collections.Generic;
using System.Threading.Tasks;
using Foni.Code.PhoneticsSystem;

namespace Foni.Code.AssetSystem
{
    public interface IAssetService
    {
        Task<List<Letter>> LoadLetters();
    }
}