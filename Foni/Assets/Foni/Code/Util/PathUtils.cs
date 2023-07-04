using System;
using System.Collections;
using System.Threading.Tasks;
using Foni.Code.Core;
using UnityEngine;

namespace Foni.Code.Util
{
    public static class PathUtils
    {
        private struct PathCache
        {
            public string PersistentDataPath;
        }

        private static PathCache _pathCache = new()
        {
            PersistentDataPath = null
        };

        private static IEnumerator Wrap(Action func)
        {
            func();
            yield break;
        }

        public static async Task<string> GetPersistentDataPath()
        {
            if (_pathCache.PersistentDataPath != null)
            {
                return _pathCache.PersistentDataPath;
            }

            await Globals.ServiceLocator.AsyncService.RunOnMainThread(() => Wrap(() =>
            {
                _pathCache.PersistentDataPath = Application.persistentDataPath;
            }));

            return _pathCache.PersistentDataPath;
        }
    }
}