using UnityEngine;

public static class Initializer
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    public static void Initialize()
    {
        Initialize<StatsManager>();
        Initialize<AudioManager>();
    }

    private static void Initialize<T>() where T : MonoBehaviour
    {
        var obj = Object.FindFirstObjectByType<T>();

        if (obj)
        {
            Object.DontDestroyOnLoad(obj);
            return;
        }

        string name = typeof(T).Name;

        GameObject prefab = Resources.Load<GameObject>(name);
        GameObject instance = Object.Instantiate(prefab);
        Object.DontDestroyOnLoad(instance);
    }
}