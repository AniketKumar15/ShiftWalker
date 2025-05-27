using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float jumpForce = 10f;


    [Header("Ground Check Settings")]
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;

    private Rigidbody2D rb;
    private bool isGrounded;
    private int jumpCount;
    public int maxJumps = 2; // 1 = normal jump, 2 = double jump

    private bool facingRight = true;
    private bool wasGrounded;
    private Animator animator;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // Ground check
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        // Reset jump count only when landing
        if (isGrounded && !wasGrounded)
        {
            jumpCount = 0;
        }
        wasGrounded = isGrounded;

        // Horizontal movement
        float moveInput = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);

        // Flip sprite
        if (moveInput > 0 && !facingRight)
            Flip();
        else if (moveInput < 0 && facingRight)
            Flip();

        animator.SetFloat("IsMoving", Mathf.Abs(moveInput));

        // Jump logic
        if (Input.GetKeyDown(KeyCode.Space) && jumpCount < maxJumps)
        {
            AudioManager.instance.Play("Jump");
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            jumpCount++;
        }

        animator.SetBool("IsJumping", !isGrounded);
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1; // Flip horizontally
        transform.localScale = scale;
    }
    // Optional: visualize ground check in editor
    void OnDrawGizmosSelected()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }
    }
}
