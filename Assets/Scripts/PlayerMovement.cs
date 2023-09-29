using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    private Rigidbody2D rb;
    public Transform hands;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void HandsLooking(Vector2 value)
    {
        Debug.Log(value);
        float angle = Mathf.Atan2(-value.x, value.y) * Mathf.Rad2Deg;
        if (value.x >= 0)
            transform.localScale = new Vector3(value.x > 0 ? 1 : -1, 1, 1);
        hands.rotation = Quaternion.Euler(0, 0, Mathf.Clamp(90 + angle, -90, 90));
    }

    public void Move(Vector2 value)
    {
        HandsLooking(value);
        rb.velocity = speed * value.normalized;
    }
}
