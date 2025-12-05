using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject startScreenUI;
    public GameObject gameOverScreenUI;
    public PlayerController playerController;
    public TextMeshProUGUI finalScoreText;
    public GameObject hudCanvas;
    void Start()
    {
        hudCanvas.SetActive(false);
        if (startScreenUI != null)
        {
            startScreenUI.SetActive(true);
        }
        Time.timeScale = 0f;
        gameOverScreenUI.SetActive(false);

    }
    public void StartGame()
    {

        startScreenUI.SetActive(false);
        hudCanvas.SetActive(true);

        Time.timeScale = 1f;

        ScoreManager.score = 0;
        FindAnyObjectByType<ScoreManager>().UpdateScoreDisplay();

    }
    public void GameOver()
    {
        // 1. Freeze the game
        Time.timeScale = 0f;

        hudCanvas.SetActive(false);

        // 2. Show the Game Over screen
        gameOverScreenUI.SetActive(true);

        // 3. Display the final score
        if (finalScoreText != null)
        {
            finalScoreText.text = ScoreManager.score.ToString();
        }

        // You may want to destroy all enemies and clean up the scene here
    }
    public void RestartGame()
    {
        // 1. Unfreeze time (must be 1.0 before loading the scene)
        Time.timeScale = 1f;

        // 2. Reload the current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}