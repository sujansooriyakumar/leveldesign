using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Input")]
    public InputAction moveAction;
    public InputAction jumpAction;

    [Header("Movement")]
    public float speed;
    public float jumpHeight;
    public float gravity = -15f;
    public int moveDirection;
    public float jumpBufferTime = 0.1f;
    public float fallGravityMuliplier = 1.5f;
    [SerializeField] private bool isGrounded;
    [SerializeField] private Vector3 moveVector = new();

    [Header("Collision")]
    public float collisionRadius = 1.0f;
    public LayerMask collisionLayers;
    [Tooltip("Distance from the player's center to its feet. The player rests exactly this far above the ground.")]
    public float halfHeight = 0.75f;
    [Tooltip("Extra ray length so the ground is still detected while resting on it.")]
    public float groundSkin = 0.05f;

    private float jumpBufferTimer;
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    [Header("Events")]
    public UnityEvent onJump;
    private void OnEnable()
    {
        moveAction.Enable();
        jumpAction.Enable();
        jumpAction.performed += Jump_Performed;
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Jump_Performed(InputAction.CallbackContext c)
    {
        Debug.Log("jump pressed");
        jumpBufferTimer = jumpBufferTime;

    }

    private void Update()
    {

        moveDirection = (int)moveAction.ReadValue<float>();
        if (moveDirection > 0)
        {
            spriteRenderer.flipX = false;
        }
        else if (moveDirection < 0)
        {
            spriteRenderer.flipX = true;
        }
        if (moveDirection != 0)
        {
            animator.SetBool("isWalking", true);
        }
        else
        {
            animator.SetBool("isWalking", false);
        }
        moveVector.x = moveDirection * speed;
        jumpBufferTimer -= Time.deltaTime;
        if ((jumpBufferTimer > 0 && isGrounded))
        {
            moveVector.y = Mathf.Sqrt(2f * Mathf.Abs(gravity) * jumpHeight);
            jumpBufferTimer = 0;
            isGrounded = false;
            animator.SetBool("isJumping", true);
            onJump?.Invoke();

        }
        float g = moveVector.y < 0f ? gravity * fallGravityMuliplier : gravity;
        if (isGrounded && moveVector.y <= 0f)
        {
            moveVector.y = 0;
        }
        else
        {
            moveVector.y = moveVector.y + g * Time.deltaTime;
        }

        transform.position += moveVector * Time.deltaTime;
        CheckCollision();

    }

    private void FixedUpdate()
    {
    }

    private void CheckCollision()
    {
        RaycastHit2D collision = Physics2D.Raycast(transform.position, Vector2.down, halfHeight + groundSkin, collisionLayers);

        // Only count as ground while falling/resting, otherwise the jump frame
        // would detect the floor and snap the player straight back down.
        if (collision && moveVector.y <= 0f)
        {
            animator.SetBool("isJumping", false);
            isGrounded = true;

            // Push-out: put the feet exactly on the surface the ray hit.
            Vector3 pos = transform.position;
            pos.y = collision.point.y + halfHeight;
            transform.position = pos;
        }
        else
        {
            isGrounded = false;
        }
    }
}