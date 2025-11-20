using UnityEngine;
using TMPro; // Include this if you want to display the score on screen (optional)

public class ScoreManager : MonoBehaviour
{
    // Make the score static so it can be accessed easily from anywhere
    public static int score = 0;

    // Optional: Reference to a TextMeshPro or UI Text element to display the score
    public TextMeshProUGUI scoreText;

    // Use Awake to ensure the score is reset before any enemies try to access it
    void Awake()
    {
        score = 0;
    }

    // Public method that the EnemyController will call
    public void AddScore(int points)
    {
        score += points;

        // Update the UI display (if scoreText is assigned)
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score.ToString();
        }

        Debug.Log("Current Score: " + score);
    }
}