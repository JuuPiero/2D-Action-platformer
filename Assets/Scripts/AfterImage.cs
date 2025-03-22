

using UnityEngine;

public class AfterImage : MonoBehaviour
{
    private SpriteRenderer sr;
    private Color color;
    private float fadeSpeed = 5f; // Tốc độ mờ dần

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    public void Setup(Sprite sprite, Vector3 position, Vector3 localScale, Color baseColor)
    {
        sr.sprite = sprite;
        transform.position = position;
        transform.localScale = localScale;
        color = baseColor;
        gameObject.SetActive(true);
    }

    void Update()
    {
        color.a -= fadeSpeed * Time.deltaTime;
        sr.color = color;

        if (color.a <= 0)
        {
            gameObject.SetActive(false); // Ẩn đi thay vì Destroy để tái sử dụng
        }
    }
}