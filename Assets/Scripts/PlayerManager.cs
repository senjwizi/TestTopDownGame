using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
    public float health;
    public float maxHealth;

    private UIManager UI;
    private PlayerAttack playerAttack;

    void Awake()
    {
        UI = FindObjectOfType<UIManager>();
        playerAttack = GetComponent<PlayerAttack>();
    }

    public void Init(Data.GameData data)
    {
        transform.position = data.playerPosition;
        health = data.playerHealth;
        maxHealth = data.playerMaxHealth;
        playerAttack.ammoSupply = data.playerAmmo;
        UI.InitUI(health, maxHealth, playerAttack.ammoSupply);
    }

    public void GetData(ref Data.GameData data)
    {
        data.playerPosition = transform.position;
        data.playerHealth = health;
        data.playerMaxHealth = maxHealth;
        data.playerAmmo = playerAttack.ammoSupply;
    }

    public void TakeDamage(float value)
    {
        health = Mathf.Clamp(health - value, 0, maxHealth);

        UI.UpdateHealth(health, maxHealth);
        if(health <= 0)
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
