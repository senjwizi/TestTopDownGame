using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public Slider health;
    public TMP_Text ammoText;


    public void UpdateHealth(float value, float max)
    {
        health.value = value / max;
    }

    public void UpdateAmmo(int value)
    {
        ammoText.text = value.ToString();
    }
}
