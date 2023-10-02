using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class InventorySlot : MonoBehaviour
{
    public TMP_Text itemAmountText;
    public Image itemImage;
    public Image selectedImage;
    public Image countBackgroud;
    public Item item;
    public int itemAmount;
    public bool isSlotEmpty = true;

    public event Action<InventorySlot> OnSelect;

    public void OnSelectSlot()
    {
        OnSelect?.Invoke(this);
    }

    private void ResetData()
    {
        itemAmountText.text = "";
        countBackgroud.enabled = false;
        itemImage.enabled = false;
        isSlotEmpty = true;
        item = null;
        itemAmount = 0;
    }

    public void ClearSlot()
    {
        ResetData();
    }

    public void InitSlot()
    {
        ResetData();
        Deselect();
    }

    public void Select()
    {
        selectedImage.enabled = true;
    }

    public void Deselect()
    {
        selectedImage.enabled = false;
    }

    public void SetItem(Item newItem)
    {
        item = newItem;
        itemImage.sprite = newItem.inventoryIcon;
        itemImage.enabled = true;
        countBackgroud.enabled = true;
        itemAmountText.text = (itemAmount += 1).ToString();
        isSlotEmpty = false;
    }
}
