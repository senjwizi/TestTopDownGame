using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public Item item;
    private InventorySystem inventorySystem;

    private void Start()
    {
        inventorySystem = FindObjectOfType<InventorySystem>();
    }

    public void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.tag == "Player")
        {
            if (inventorySystem.TryAddItem(item))
                Destroy(gameObject);
        }
    }
}