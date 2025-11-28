using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static int score = 0;
    public TextMeshProUGUI scoreText;
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
        Debug.Log("Current Score: " + score);
    }
}