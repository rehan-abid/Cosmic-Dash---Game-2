using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public Transform groundCheck;
    public LayerMask groundLayer;
    public float groundCheckRadius = 0.2f;

    private Rigidbody2D rb;
    private bool isGrounded;
    private PlayerControls playerControls;
    private Vector2 currentMoveInput;
    private GameManager gameManager;
    private SpriteRenderer spriteRenderer;
    public GameObject jumpDustFX;

    void Awake()
    {
        playerControls = new PlayerControls();
        playerControls.Gameplay.Move.performed += ctx => currentMoveInput = ctx.ReadValue<Vector2>();
        playerControls.Gameplay.Move.canceled += ctx => currentMoveInput = Vector2.zero;
        playerControls.Gameplay.Jump.performed += OnJump;
        gameManager = FindAnyObjectByType<GameManager>();
    }
    void OnEnable()
    {
        playerControls.Enable();
    }
    void OnDisable()
    {
        playerControls.Disable();
    }
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    void Update()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        HandleSpriteFlipping();
    }
    public void OnJump(InputAction.CallbackContext context)
    {
        if (isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            if(jumpDustFX != null)
            {
                Instantiate(jumpDustFX, groundCheck.position, Quaternion.identity);
            }

            if (gameManager != null)
            {
                gameManager.PlaySFX(gameManager.jumpSFX);
            }
        }
    }
    void FixedUpdate()
    {
        // 8. Apply Horizontal Movement using the stored input vector (currentMoveInput)
        float moveInputX = currentMoveInput.x;

        rb.linearVelocity = new Vector2(moveInputX * moveSpeed, rb.linearVelocity.y);
    }

    private void HandleSpriteFlipping()
    {
        float inputX = currentMoveInput.x;
        if (inputX < -0.01f)
        {
            if (spriteRenderer != null)
            {
                spriteRenderer.flipX = true;

            }

        }
        else
        {
            if (spriteRenderer != null)
            {
                spriteRenderer.flipX = false;
            }
        }

    }
}
