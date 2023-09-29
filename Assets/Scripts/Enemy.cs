using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public LayerMask playerMask;
    private float speed;
    public float freezSpeed;
    public float normalSpeed;
    public float detectRadius;
    public float borderRadius;
    public float attackRadius;
    private bool isDetect, isAttack, isBorder;

    private float health; 
    public float maxHealth;
    public float damage;
    private Rigidbody2D rb;
    private Transform player;
    private float timeToAttack;
    public float attackRate;

    public Item[] whatCanDrop;
    public Slider healthBar;
    public Transform render;

    public float forceDamping;
    public Vector2 forceToApply;
    public float freezTime;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindWithTag("Player").transform;
    }

    private void Start()
    {
        speed = normalSpeed;
        healthBar.value = health = maxHealth;
    }

    private void Update()
    {
        isDetect = Physics2D.OverlapCircle(transform.position, detectRadius, playerMask);
        isBorder = Physics2D.OverlapCircle(transform.position, borderRadius, playerMask);
        isAttack = Physics2D.OverlapCircle(transform.position, attackRadius, playerMask);
    }

    private void FixedUpdate()
    {        
        if(isDetect && !isAttack && !isBorder)
            Follow();
        else if (isDetect && isAttack && !isBorder)
            Attack();
    }

    private void Follow()
    {
        Vector2 direction = -(transform.position - player.position).normalized;
        Vector2 moveDirection = direction * speed;
        //moveDirection += forceToApply;
        forceToApply /= forceDamping;
        if(Mathf.Abs(forceToApply.x) <= 0.01f && Mathf.Abs(forceToApply.y) <= 0.01f)
            forceToApply = Vector2.zero;
        rb.velocity = moveDirection;
        render.localScale = new Vector3(transform.position.x - player.position.x > 0 ? -1 : 1, 1, 1);
    }

    private void Attack()
    {
        if(Time.time >= timeToAttack)
        {
            timeToAttack = Time.time + 1 / attackRate;
            player.transform.TryGetComponent(out PlayerManager playerManager);
            playerManager.TakeDamage(damage);        
        }
    }

    public void TakeDamage(float value, Vector2 force)
    {
        health -= value;
        healthBar.value = health / maxHealth;
        if (health <= 0)
            StartCoroutine(Die());
        //else
            //forceToApply += force;
        speed = freezSpeed;
        //rb.velocity = Vector2.zero;
        Invoke("ResetFreez", freezTime);
    }

    private void ResetFreez()
    {
        speed = normalSpeed;
    }

    IEnumerator Die()
    {
        Instantiate(whatCanDrop[Random.Range(0, whatCanDrop.Length)].itemObject, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(0f);
        Destroy(gameObject);
    }
}
