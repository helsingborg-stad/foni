using System;
using UnityEngine;

namespace Foni.Code.ProfileSystem
{
    [Serializable]
    public struct ProfileData
    {
        public string name;
        public string icon;
        public StatisticsData statistics;

        public readonly string Serialize()
        {
            return JsonUtility.ToJson(this);
        }

        public static ProfileData Deserialize(string profileDataJson)
        {
            return JsonUtility.FromJson<ProfileData>(profileDataJson);
        }
    }
}