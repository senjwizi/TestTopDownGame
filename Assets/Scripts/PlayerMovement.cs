using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    private Rigidbody2D rb;
    public Transform hands;
    public Transform render;
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void HandsLooking(Vector3 value)
    {
        Vector3 dir = value - transform.position;
        float angle = Mathf.Atan2(-dir.x, dir.y) * Mathf.Rad2Deg;
        hands.rotation = Quaternion.Euler(0, 0, 90 + angle);
        hands.localScale = new Vector3(1, (angle > 0 && angle < 180) ? -1 : 1, 1);
    }

    public void Move(Vector2 value)
    {
        if (value.x >= 0)
            render.localScale = new Vector3(value.x > 0 ? 1 : -1, 1, 1);
        rb.velocity = speed * value.normalized;
    }
}
