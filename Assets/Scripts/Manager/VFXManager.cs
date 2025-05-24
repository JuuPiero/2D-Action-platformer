using System.Collections.Generic;
using UnityEngine;

public class VFXManager : Singleton<VFXManager>
{
    // public static VFXManager Instance;
    public List<GameObject> vfxPrefabs = new();
    private Dictionary<string, GameObject> vfxDict = new Dictionary<string, GameObject>();

    // private void Awake()
    // {
    //     if (Instance == null)
    //     {
    //         Instance = this;
    //         DontDestroyOnLoad(gameObject);
    //     }
    //     else
    //     {
    //         Destroy(gameObject);
    //     }
    // }
    void Start()
    {
        foreach (var vfx in vfxPrefabs)
        {
            vfxDict[vfx.name] = vfx;
        }
    }

    public void PlayEffect(string effectName, Vector3 position, float lifetime = 0.2f)
    {
        if (vfxDict.TryGetValue(effectName, out GameObject prefab))
        {
            GameObject effect = Instantiate(prefab, position, Quaternion.identity);
            Destroy(effect, lifetime);
        }
    }


    public GameObject InstantiateEffect(string effectName, Vector3 position)
    {
        if (vfxDict.TryGetValue(effectName, out GameObject prefab))
        {
            GameObject effect = Instantiate(prefab, position, Quaternion.identity);
            return effect;
        }
        return null;
    }
}