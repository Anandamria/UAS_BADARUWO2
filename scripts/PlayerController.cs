using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 3f;
    public float jumpForce = 4f;
    public float blinkDuration = 2f;
    public float blinkInterval = 0.2f;

    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private PotionThrower potionThrower;

    private bool isGrounded = true;
    private bool isInvincible = false;
    private bool isThrowing = false;
    private float moveInput = 0f;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        potionThrower = GetComponent<PotionThrower>(); 

        if (GameManager.Instance != null)
        {
            UIManager.Instance?.UpdateHeartUI(GameManager.Instance.currentHearts);
            UIManager.Instance?.UpdatePotionUI(GameManager.Instance.potionCount);
        }
    }

    private void Update()
    {
        HandleThrow();

        if (!isThrowing)
        {
            moveInput = 0f;
            if (Input.GetKey(KeyCode.D)) moveInput = 1f;
            else if (Input.GetKey(KeyCode.A)) moveInput = -1f;

            HandleJump();
            HandleCrouch();
        }

        if (transform.position.y < -5f && !GameManager.Instance.isGameOver)
        {
            GameManager.Instance.TriggerGameOver();
        }
    }

    private void FixedUpdate()
    {
        if (!isThrowing)
        {
            rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);

            if (moveInput != 0)
                spriteRenderer.flipX = moveInput < 0;

            animator.SetBool("isRunning", moveInput != 0);
        }
    }

    private void HandleJump()
    {
        if (Input.GetKeyDown(KeyCode.W) && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            animator.SetBool("isJumping", true);
            isGrounded = false;
        }
    }

    private void HandleCrouch()
    {
        animator.SetBool("isCrouching", Input.GetKey(KeyCode.S));
    }

    private void HandleThrow()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isThrowing)
        {
            StartCoroutine(ThrowCoroutine());
        }
    }

    private IEnumerator ThrowCoroutine()
    {
        isThrowing = true;
        animator.SetTrigger("Throw");

        yield return new WaitForSeconds(0.2f); // tunggu sebelum lempar


        yield return new WaitForSeconds(0.3f); // tunggu sisa animasi

        isThrowing = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            StartCoroutine(DelayLanding());
        }
    }

    private IEnumerator DelayLanding()
    {
        yield return new WaitForSeconds(0.05f);
        isGrounded = true;
        animator.SetBool("isJumping", false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy") && !isInvincible)
        {
            GameManager.Instance?.TakeDamage(1);
            StartCoroutine(BlinkEffect());
        }

        if (other.CompareTag("Potion"))
        {
            GameManager.Instance?.AddPotion();
            Destroy(other.gameObject);
        }
    }

    private IEnumerator BlinkEffect()
    {
        isInvincible = true;
        float elapsed = 0f;
        Color originalColor = spriteRenderer.color;

        while (elapsed < blinkDuration)
        {
            spriteRenderer.color = new Color(originalColor.r, originalColor.g, originalColor.b, 0.2f);
            yield return new WaitForSeconds(blinkInterval);

            spriteRenderer.color = new Color(originalColor.r, originalColor.g, originalColor.b, 1f);
            yield return new WaitForSeconds(blinkInterval);

            elapsed += blinkInterval * 2;
        }

        spriteRenderer.color = originalColor;
        isInvincible = false;
    }
}
