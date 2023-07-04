using System.Collections.Generic;
using UnityEngine;

namespace Foni.Code.ProfileSystem
{
    public class AvatarIconMapper : MonoBehaviour
    {
        [SerializeField] private List<Sprite> avatars;

        public Sprite GetSprite(string iconName)
        {
            var sprite = avatars.Find(avatar => avatar.name == iconName);
            if (sprite == null)
            {
                Debug.LogErrorFormat("No avatar icon mapped for '{0}'", iconName);
            }

            return sprite;
        }

        public List<Sprite> GetSprites()
        {
            return avatars;
        }
    }
}