using UnityEngine;

public class EnemyController : MonoBehaviour
{
    enum MovementPattern
    {
        Horizontal,
        Vertical,
        Circular
    }

    [SerializeField] float moveRange = 0f;
    [SerializeField] float frequency = 0f;
    [SerializeField] bool move = false;
    [SerializeField] bool negativeMovement = false;
    [SerializeField] MovementPattern pattern = MovementPattern.Horizontal;

    private float startX;
    private float startY;
    private Rigidbody2D rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        startX = rb.position.x; startY = rb.position.y;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!move) return;
        float targetX = startX;
        float targetY = startY;
        float posChangeX = Mathf.Cos(Time.time * frequency) * moveRange;
        float posChangeY = (Mathf.Sin(Time.time * frequency) + 1) * moveRange;
        posChangeY *= negativeMovement ? -1 : 1;
        if (pattern == MovementPattern.Horizontal)
        {
            targetX = startX + posChangeY;
        } else if (pattern == MovementPattern.Vertical)
        {
            targetY = startY + posChangeY;
        } else if (pattern == MovementPattern.Circular)
        {
            targetX = startX + posChangeX;
            targetY = startY + posChangeY;
        }
        float horizontalVelocity = (targetX - rb.position.x) / Time.fixedDeltaTime;
        float verticalVelocity = (targetY - rb.position.y) / Time.fixedDeltaTime;

        // Apply velocity: constant forward speed on X, oscillating speed on Y
        rb.linearVelocity = new Vector2(horizontalVelocity, verticalVelocity);
    }
}
