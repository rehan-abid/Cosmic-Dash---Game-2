using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static int score = 0;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI hudScoreText;
    void Awake()
    {
        score = 0;
    }
    public void AddScore(int points)
    {
        score += points;
        if (scoreText != null)
        {
            scoreText.text = "Score:" + score.ToString();
        }
        UpdateScoreDisplay();
        Debug.Log("Current Score: " + score);
    }
    public void UpdateScoreDisplay()
    {
        if (hudScoreText != null)
        {
            hudScoreText.text =score.ToString();
        }
    }
}