using UnityEngine;
public class EnemyController : MonoBehaviour
{
    private float moveSpeed;
    private Rigidbody2D rb;
    private ScoreManager scoreManager;
    private GameManager gameManager;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        scoreManager = FindAnyObjectByType<ScoreManager>();
        gameManager = FindAnyObjectByType<GameManager>();
    }
    public void SetSpeed(float speed)
    {
        moveSpeed = speed;
    }
    void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(-moveSpeed, 0);
        if (transform.position.x < -15f)
        {
            Destroy(gameObject);
        }

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the collision involves the Player
        if (collision.gameObject.CompareTag("Player"))
        {
            // FATAL HIT: Player hit side or bottom.
            Debug.Log("FATAL COLLISION! Game Over!");

            // CRITICAL CHANGE: Call the dedicated Game Over function on the manager
            if (gameManager != null)
            {
                gameManager.GameOver(); // This function handles Time.timeScale = 0f and shows the UI.
            }
            else
            {
                // Fallback in case the manager isn't found
                Time.timeScale = 0f;
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
       if (other.CompareTag("Player"))
        {
            if (scoreManager != null)
            {
                scoreManager.AddScore(1);
            }
            Rigidbody2D playerRb = other.GetComponent<Rigidbody2D>();
            if (playerRb != null)
            {
                playerRb.linearVelocity = new Vector2(playerRb.linearVelocity.x, 6f);
            }
            Destroy(gameObject);
            if(gameManager != null)
            {
                gameManager.PlaySFX(gameManager.stompSFX);
            }
        }
    }
}
