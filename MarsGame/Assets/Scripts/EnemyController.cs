using UnityEngine;

public class EnemyController : MonoBehaviour
{
    // Movement speed property
    public float moveSpeed = 3f;

    private Rigidbody2D rb;
    private ScoreManager scoreManager;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // Find the ScoreManager instance in the scene using the modern method
        scoreManager = FindAnyObjectByType<ScoreManager>();
    }

    void FixedUpdate()
    {
        // Continuous left movement for the Kinematic Rigidbody
        rb.linearVelocity = new Vector2(-moveSpeed, 0);

        // Destroy enemy if it moves too far off-screen to the left
        if (transform.position.x < -15f)
        {
            Destroy(gameObject);
        }
    }

    // --- 1. GAME OVER (Hit the solid Body Collider) ---
    // This function runs when the player hits the non-trigger BoxCollider2D (sides/bottom).
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // FATAL HIT: Player hit side or bottom.
            Debug.Log("FATAL COLLISION! Game Over!");
            Time.timeScale = 0f; // Freeze the game
        }
    }

    // --- 2. STOMP SUCCESS (Hit the EdgeCollider2D Trigger) ---
    // This function runs when the player hits the Is Trigger collider (the line on top).
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the object entering the trigger is the Player
        if (other.CompareTag("Player"))
        {
            // STOMP SUCCESS: Player Hit Top (Gets Point)
            if (scoreManager != null)
            {
                scoreManager.AddScore(1); // Increase score by 1 point
            }

            // Apply bounce to the Player
            Rigidbody2D playerRb = other.gameObject.GetComponent<Rigidbody2D>();
            if (playerRb != null)
            {
                // Set a small upward velocity for the bounce effect
                playerRb.linearVelocity = new Vector2(playerRb.linearVelocity.x, 7f);
            }

            // Destroy the enemy object
            Destroy(gameObject);
        }
    }
}