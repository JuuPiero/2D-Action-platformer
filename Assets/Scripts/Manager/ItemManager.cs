using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public GameObject itemPrefab;
    public GameObject chestPrefab;

    public List<ItemDataSO> items;
    public static ItemManager Instance { get; private set; }
    
    [SerializeField] [Range(1, 999)] private int _spawnItemRange = 1; 

    void Awake()
    {
        Instance = this;
    }

    public void SpawnItem(ItemDataSO data, int quantity, Vector3 position)
    {
        GameObject itemGO = Instantiate(itemPrefab);
        Item item = itemGO.GetComponent<Item>();
        item.SetData(data);
        item.quantity = quantity;
        itemGO.transform.position = position;
    }

    public void SpawnItem(Vector3 position)
    {
        ItemDataSO r = items[Random.Range(0, items.Count)];
        GameObject itemGO = Instantiate(itemPrefab);
        Item item = itemGO.GetComponent<Item>();
        var rb = item.GetComponent<Rigidbody2D>();
        item.SetData(r);
        if (r.isStackable)
            item.quantity = Random.Range(1, _spawnItemRange);
        itemGO.transform.position = position;
        rb?.AddForce(Vector2.up * 3f, ForceMode2D.Impulse);
    }
}
