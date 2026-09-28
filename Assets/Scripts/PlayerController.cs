using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    [SerializeField] float speed;
    [SerializeField] GameManager gm;

    private Rigidbody2D rb;
    float moveInputX = 0f;
    float moveInputY = 0f;

    private bool dead = false;
    private Animator animator;
    private AudioSource deathSound;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        deathSound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        GetMoveInput();
    }

    void GetMoveInput()
    {
        moveInputX = 0;
        moveInputY = 0;
        if (dead) return;

        if (Keyboard.current.dKey.isPressed || Keyboard.current.rightArrowKey.isPressed)
        {
            moveInputX += 1;
        }
        else if (Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed)
        {
            moveInputX -= 1;
        }

        if (Keyboard.current.wKey.isPressed || Keyboard.current.upArrowKey.isPressed)
        {
            moveInputY += 1;
        }
        else if (Keyboard.current.sKey.isPressed || Keyboard.current.downArrowKey.isPressed)
        {
            moveInputY -= 1;
        }
    }

    public bool IsDead()
    {
        return dead;
    }

    public void MarkDead()
    {
        dead = true;
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(speed * moveInputX, speed * moveInputY);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Pufferfish") && !dead)
        {
            dead = true;
            deathSound.Play();
            animator.SetTrigger("Die");
            gm.OnPlayerDied(true);
        }
    }
}
