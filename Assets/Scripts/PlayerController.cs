using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Hareket Ayarları")]
    public float moveSpeed = 5f;
    public float jumpForce = 12f;

    [Header("Animasyon ve Sprite")]
    public Animator anim;
    private SpriteRenderer spriteRenderer;

    private Rigidbody2D rb;
    private bool canJump;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        float xInput = Input.GetAxis("Horizontal");

        // Sprite yönü
        if (xInput != 0)
            spriteRenderer.flipX = xInput < 0;

        // Animasyon
        if (anim != null)
            anim.SetFloat("horizontal", Mathf.Abs(xInput));

        // Zıplama
        if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space)) && canJump)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            canJump = false; // zıpladıktan sonra tekrar yere temas bekle
        }
    }

    void FixedUpdate()
    {
        float xInput = Input.GetAxis("Horizontal");
        rb.linearVelocity = new Vector2(xInput * moveSpeed, rb.linearVelocity.y);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Sadece Platform tag'li objelere çarptığında canJump true
        if (collision.collider.CompareTag("Platform"))
        {
            canJump = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Platform"))
        {
            canJump = false;
        }
    }
}