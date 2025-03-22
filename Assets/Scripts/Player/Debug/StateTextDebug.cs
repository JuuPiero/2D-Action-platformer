using UnityEngine;

public class StateTextDebug : MonoBehaviour 
{
    Vector3 originalScale;

    void Start() {
        originalScale = transform.lossyScale;
    }

    void LateUpdate() {
            transform.localScale = new Vector3(
            originalScale.x / transform.parent.lossyScale.x,
            originalScale.y / transform.parent.lossyScale.y,
            originalScale.z / transform.parent.lossyScale.z
        );
    }
}
