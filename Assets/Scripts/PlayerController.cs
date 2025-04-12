using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public bool canMove = true;

    private PlayerControls controls;
    private Vector2 moveInput;
    private Rigidbody2D rb;

    public float moveSpeed = 5f;
    public float jumpForce = 10f;

    public Transform groundCheck;
    public LayerMask groundLayer;
    public float groundCheckRayLength = 0.1f;

    private Animator animator;
    private SpriteRenderer spriteRenderer;

    private bool jumpRequested;
    private bool jumpAfterUnlock = false;

    private void Awake()
    {
        controls = new PlayerControls();
        controls.Player.Move.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        controls.Player.Move.canceled += ctx => moveInput = Vector2.zero;
        controls.Player.Jump.performed += ctx => jumpRequested = true;
    }

    private void OnEnable() => controls.Enable();
    private void OnDisable() => controls.Disable();

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        if (!canMove)
        {
            rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
            return;
        }

        // If movement just got enabled, trigger auto jump
        if (jumpAfterUnlock)
        {
            if (IsGrounded())
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
                jumpAfterUnlock = false;
            }
        }

        // Horizontal movement
        rb.linearVelocity = new Vector2(moveInput.x * moveSpeed, rb.linearVelocity.y);

        // Flip sprite
        if (moveInput.x != 0)
            spriteRenderer.flipX = moveInput.x < 0;

        // Jump
        if (jumpRequested && IsGrounded())
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }
        jumpRequested = false;

        // Animation
        animator.SetFloat("Speed", Mathf.Abs(moveInput.x));
    }

    private bool IsGrounded()
    {
        RaycastHit2D hit = Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckRayLength, groundLayer);
        return hit.collider != null;
    }

    // Call this from the camera script when cinematic ends
    public void UnlockMovementWithJump()
    {
        canMove = true;
        jumpAfterUnlock = true;
    }
}
