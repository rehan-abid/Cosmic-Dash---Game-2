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
    }
    void Update()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
    }
    public void OnJump(InputAction.CallbackContext context)
    {
        if (isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
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

}
