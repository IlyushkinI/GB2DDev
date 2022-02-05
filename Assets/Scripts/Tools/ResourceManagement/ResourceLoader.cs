using UnityEngine;

public static class ResourceLoader<T> where T: Object
{
    public static T LoadPrefab(ResourcePath path)
    {
        return Resources.Load<T>(path.PathResource);
    }
} 
