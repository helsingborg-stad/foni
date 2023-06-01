using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Foni.Code.DataSourceSystem.Implementation.StreamingAssetDataSource;
using Foni.Code.PhoneticsSystem;
using Foni.Code.ServicesSystem;

namespace Foni.Code.AssetSystem.Implementation
{
    public class DeviceLocalAssetService : IAssetService, IService
    {
        private readonly StreamingAssetsDataSource _dataSource;

        public DeviceLocalAssetService()
        {
            _dataSource = new StreamingAssetsDataSource();
        }

        public async Task<List<Letter>> LoadLetters()
        {
            var list =
                (await _dataSource.Load<PhoneticsSerialization.SerializedLetterRoot>("letters.json"))
                .letters
                .ToList()
                .ConvertAll(PhoneticsSerialization.DeserializeLetter);

            return list;
        }
    }
}