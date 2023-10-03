using UnityEngine;

public class InventorySystem : MonoBehaviour
{
    public InventorySlot[] slots;
    public InventorySlot selectedSlot;
    private UIManager managerUI;
    public Item addingIntem;
    private int slotsCount = 3;

    private void Start()
    {
        managerUI = FindObjectOfType<UIManager>();
    }

    public bool TryAddItem(Item newItem)
    {
        InventorySlot emptySlot = null;
        
        for(int i = 0; i < slots.Length; i++)
        {
            if (!slots[i].isSlotEmpty && slots[i].item == newItem && slots[i].itemAmount < slots[i].item.maxCountInStack)
            {
                slots[i].SetItem(newItem);
                UpdateDeleteButton();
                return true;
            }
            else if (slots[i].isSlotEmpty && emptySlot == null)
            {
                emptySlot = slots[i];
            }
        }

        if(emptySlot != null)
        {
            emptySlot.SetItem(newItem);
            UpdateDeleteButton();
            return true;
        }
        return false;
    }

    public void DeleteItem()
    {
        if(selectedSlot != null)
        {
            selectedSlot.ClearSlot();
            UpdateDeleteButton();
        }
    }

    private void SelectSlot(InventorySlot slot)
    {
        if (selectedSlot != null)
            selectedSlot.Deselect();

        if (slot != selectedSlot)
        {
            slot.Select();
            selectedSlot = slot;
        }
        else
            selectedSlot = null;

        UpdateDeleteButton();
    }

    private void UpdateDeleteButton()
    {
        if (selectedSlot == null || selectedSlot.isSlotEmpty)
            managerUI.InhibitDeleteButton();
        else if (selectedSlot != null)
            managerUI.InvokeDeleteButton();
    }

    public void Init(Data.GameData data)
    {
        for(int i = 0; i < slotsCount; i++)
        {
            slots[i].InitSlot();
            slots[i].OnSelect += SelectSlot;
            if(data.items != null && data.items[i] != null)
                slots[i].SetItem(data.items[i], data.amount[i]);
        }
    }

    public void GetData(ref Data.GameData data)
    {
        data.slotsCount = slotsCount;
        GetArrayData(ref data.items, ref data.amount);
    }

    private void GetArrayData(ref Item[] outItems, ref int[] outAmount)
    {
        outItems = new Item[slotsCount];
        outAmount = new int[slotsCount];
        for(int i = 0; i < slotsCount; i++)
        {
            outItems[i] = slots[i].item;
            outAmount[i] = slots[i].itemAmount;
        }
    }
}
