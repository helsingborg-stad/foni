using UnityEngine;

namespace Foni.Code.Core
{
    public class Startup : MonoBehaviour
    {
        private void Awake()
        {
            Globals.EnsureGlobalsObject();
        }
    }
}