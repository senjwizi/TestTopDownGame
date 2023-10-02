using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySystem : MonoBehaviour
{
    public InventorySlot[] slots;
    public InventorySlot selectedSlot;
    private UIManager managerUI;

    public Item addingIntem;
    public int amount;

    private void Start()
    {
        managerUI = FindObjectOfType<UIManager>();

        LoadInventory();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            TestAdd();
        }
    }

    private void TestAdd()
    {
        bool canAdd = TryAddItem(addingIntem);
        if (!canAdd)
            Debug.Log("Нет места в инвенторе");

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

    private void LoadInventory()
    {
        for(int i = 0; i < slots.Length; i++)
        {
            slots[i].InitSlot();
            slots[i].OnSelect += SelectSlot;

            //Load saved data;
            //slots[i].Init(newItem, amount);
        }
    }

    public void SaveInventory()
    {

    }

}
