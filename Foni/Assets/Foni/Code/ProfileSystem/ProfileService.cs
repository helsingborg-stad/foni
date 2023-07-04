using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Foni.Code.SaveSystem;
using UnityEngine;

namespace Foni.Code.ProfileSystem
{
    public class ProfileService : IProfileService
    {
        [Serializable]
        private struct ProfilesWrapper
        {
            public List<ProfileData> profiles;
        }

        private readonly ISaveService _saveService;
        private List<ProfileData> _profileCache;
        private const string ProfilesSaveID = "profiles";

        public ProfileService(ISaveService saveService)
        {
            _saveService = saveService;
            _profileCache = null;
        }

        public async Task<List<ProfileData>> GetAllProfiles()
        {
            if (_profileCache != null)
            {
                return _profileCache;
            }

            try
            {
                var rawProfileContent = await _saveService.Load(ProfilesSaveID);
                var profiles = JsonUtility.FromJson<ProfilesWrapper>(rawProfileContent).profiles;
                _profileCache = profiles;
                Debug.LogFormat("Loaded {0} profile(s)", _profileCache.Count);
            }
            catch (FileNotFoundException)
            {
                _profileCache = new List<ProfileData>();
            }

            return _profileCache;
        }

        public Task UpdateProfile(ProfileData profileData)
        {
            var profileIndex = _profileCache.FindIndex(profile => profile.name == profileData.name);
            if (profileIndex != -1)
            {
                _profileCache[profileIndex] = profileData;
            }
            else
            {
                _profileCache.Add(profileData);
            }

            return SaveProfiles();
        }

        public Task RemoveProfile(string id)
        {
            var profileIndex = _profileCache.FindIndex(profile => profile.name == id);
            if (profileIndex == -1) return Task.CompletedTask;

            _profileCache.RemoveAt(profileIndex);
            return SaveProfiles();
        }

        public bool ContainsProfile(string id)
        {
            return _profileCache.FindIndex(profile => profile.name == id) >= 0;
        }

        private Task SaveProfiles()
        {
            _profileCache.Sort((a, b) => string.Compare(a.name, b.name, StringComparison.Ordinal));
            Debug.LogFormat("Saving {0} profile(s)", _profileCache.Count);
            return _saveService.Save(ProfilesSaveID, JsonUtility.ToJson(new ProfilesWrapper
            {
                profiles = _profileCache
            }));
        }
    }
}