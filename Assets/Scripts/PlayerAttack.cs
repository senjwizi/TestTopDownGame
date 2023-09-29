using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public LayerMask enemyLayer;
    public float enemyDetectRadius;
    public float damage;
    public float damageForce;
    public int ammoSupply;

    public float fireRate;
    private float timeToFire = 0;
    public bool shooting;
    private PlayerMovement playerMovement;
    private UIManager UI;

    void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
        UI = FindObjectOfType<UIManager>();
    }

    void Update()
    {
        Fire();
    }

    public void Fire()
    {
        if (shooting && ammoSupply > 0)
        {
            if(Time.time >= timeToFire)
            {
                timeToFire = Time.time + 1 / fireRate;

                Collider2D inArea = Physics2D.OverlapCircle(transform.position, enemyDetectRadius, enemyLayer);
                if(inArea != null)
                {
                    inArea.transform.TryGetComponent(out Enemy enemy);
                    playerMovement.HandsLooking(inArea.transform.position);
                    enemy.TakeDamage(damage, damageForce * (inArea.transform.position - transform.position));
                }
                ammoSupply--;
                UI.UpdateAmmo(ammoSupply);
            }
        }
    }
}
