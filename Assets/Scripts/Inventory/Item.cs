
using TMPro;
using UnityEngine;
public class Item : MonoBehaviour
{

    public ItemDataSO data;
    public int quantity = 1; // default

    public SpriteRenderer spriteRenderer;
    public TextMeshPro textQuantity;

    void Awake()
    {
        textQuantity = GetComponentInChildren<TextMeshPro>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    void Start()
    {
        if (textQuantity != null)
        {
            textQuantity.text = "x" + quantity.ToString();
        }
        if (spriteRenderer != null)
        {
            spriteRenderer.sprite = data.icon;
        }
    }
    void OnCollisionEnter2D(Collision2D col)
    {
        var player = col.gameObject.GetComponent<Player>();
        if (player != null)
        {
            player.Inventory.AddItem(this);
            Destroy(gameObject);
        }
    }

    public void SetData(ItemDataSO data)
    {
        this.data = data;
        if (textQuantity != null)
        {
            textQuantity.text = "x" + quantity.ToString();
        }
        if (spriteRenderer != null)
        {
            spriteRenderer.sprite = data.icon;
        }
    }

}