using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public LayerMask playerMask;
    private float speed;
    public float freezSpeed;
    public float normalSpeed;
    public float detectRadius;
    public float attackRadius;
    private bool isDetect, isAttack;

    public float health; 
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

    [HideInInspector]
    public EnemyManager enemyManager;
    private Transform droppedItems;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindWithTag("Player").transform;
    }

    private void Start()
    {
        speed = normalSpeed;
        healthBar.value = health;
        UpdateHealthbar();
    }

    private void Update()
    {
        isDetect = Physics2D.OverlapCircle(transform.position, detectRadius, playerMask);
        isAttack = Physics2D.OverlapCircle(transform.position, attackRadius, playerMask);
    }

    private void FixedUpdate()
    {        
        if(isDetect && !isAttack)
            Follow();
        else if (isDetect && isAttack)
            Attack();
    }

    private void Follow()
    {
        Vector2 direction = -(transform.position - player.position).normalized;
        Vector2 moveDirection = speed * Time.fixedDeltaTime * direction;
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
        rb.velocity = Vector2.zero;        
    }

    public void TakeDamage(float value, Vector2 force)
    {
        health -= value;
        UpdateHealthbar();
        if (health <= 0)
            Die();
        speed = freezSpeed;
        Invoke("ResetFreez", freezTime);
    }

    void UpdateHealthbar()
    {
        healthBar.value = health / maxHealth;
    }

    private void ResetFreez()
    {
        speed = normalSpeed;
    }

    private void Die()
    {
        Instantiate(whatCanDrop[Random.Range(0, whatCanDrop.Length)].itemObject, transform.position, Quaternion.identity, droppedItems);
        enemyManager.RemoveFromList(gameObject);
        enemyManager.enemiesCount--;
        Destroy(gameObject);
    }
}
