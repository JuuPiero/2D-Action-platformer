

using UnityEngine;

public class CameraShake : MonoBehaviour {
    public static CameraShake Instance;

    void Awake()
    {
        Instance = this;
    }
}