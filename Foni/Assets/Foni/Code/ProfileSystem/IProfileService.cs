using System.Collections.Generic;
using System.Threading.Tasks;
using Foni.Code.ServicesSystem;

namespace Foni.Code.ProfileSystem
{
    public interface IProfileService : IService
    {
        public Task<List<ProfileData>> GetAllProfiles();
        public Task UpdateProfile(ProfileData profileData);
        public Task RemoveProfile(string id);
        public bool ContainsProfile(string id);
    }
}