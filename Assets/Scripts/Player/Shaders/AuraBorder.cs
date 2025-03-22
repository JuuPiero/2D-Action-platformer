using UnityEngine;

public class AuraBorder : MonoBehaviour
{
    public SpriteRenderer targetSpriteRenderer; 
    private SpriteRenderer borderSpriteRenderer;

    void Awake()
    {
        borderSpriteRenderer = GetComponent<SpriteRenderer>();
        transform.localScale = transform.localScale + new Vector3(.2f, 0.05f, 0f);
    }

    void Update()
    {
        if (targetSpriteRenderer == null) return;

        borderSpriteRenderer.sprite = targetSpriteRenderer.sprite;
    }
}
