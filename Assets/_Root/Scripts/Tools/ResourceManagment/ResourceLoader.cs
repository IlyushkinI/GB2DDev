using UnityEngine;

namespace RaceMobile.Tools.ResourceManagment
{
    internal static class ResourceLoader
    {
        public static GameObject LoadPrefab(ResourcePath path)
        {
            return Resources.Load<GameObject>(path.PathResource);
        }
    }
}
