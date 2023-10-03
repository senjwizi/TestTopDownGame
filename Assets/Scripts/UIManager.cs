using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public Slider health;
    public TMP_Text ammoText;
    public GameObject deleteItemButton;

    private void Start()
    {
        InhibitDeleteButton();
    }

    public void InvokeDeleteButton()
    {
        deleteItemButton.SetActive(true);
    }

    public void InhibitDeleteButton()
    {
        deleteItemButton.SetActive(false);
    }

    public void InitUI(float health, float maxHealth, int ammo)
    {
        UpdateHealth(health, maxHealth);
        UpdateAmmo(ammo);
    }


    public void UpdateHealth(float value, float max)
    {
        health.value = value / max;
    }

    public void UpdateAmmo(int value)
    {
        ammoText.text = value.ToString();
    }
}
