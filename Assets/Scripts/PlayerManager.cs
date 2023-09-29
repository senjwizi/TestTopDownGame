using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
    private float health;
    public float maxHealth;

    private UIManager UI;

    void Awake()
    {
        UI = FindObjectOfType<UIManager>();
    }

    void Start()
    {
        health = maxHealth;
    }

    public void TakeDamage(float value)
    {
        health = Mathf.Clamp(health - value, 0, maxHealth);

        UI.UpdateHealth(health, maxHealth);
        if(health <= 0)
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
