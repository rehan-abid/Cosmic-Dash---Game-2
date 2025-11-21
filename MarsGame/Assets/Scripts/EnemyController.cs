using UnityEngine;
public class EnemyController : MonoBehaviour
{
    private float moveSpeed;
    private Rigidbody2D rb;
    private ScoreManager scoreManager;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        scoreManager = FindAnyObjectByType<ScoreManager>();
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
        if (collision.gameObject.CompareTag("Player"))
        {

            Debug.Log("Fatal Collision ! GAME OVER ");
            Time.timeScale = 0;
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
        }
    }
}
