using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Items/Create new")]
public class Item : ScriptableObject
{
    public string itemName;
    public int maxCountInStack;
    public Sprite inventoryIcon;
    public GameObject itemObject;
}
