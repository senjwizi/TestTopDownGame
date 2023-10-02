using Unity.VisualScripting;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public Item item;
    private SpriteRenderer spriteRenderer;
    private InventorySystem inventorySystem;

    private void Start()
    {
        inventorySystem = FindObjectOfType<InventorySystem>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = item.itemObject;
    }

    public void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.tag == "Player")
        {
            inventorySystem.TryAddItem(item);
            Destroy(gameObject);
        }
    }
}