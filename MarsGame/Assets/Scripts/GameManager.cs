using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public GameObject startScreenUI;
    public GameObject gameOverScreenUI;
    public PlayerController playerController;
    public TextMeshProUGUI finalScoreText;
    public GameObject hudCanvas;
    public AudioSource backgroundMusicSource;
    public AudioSource sfxSource;
    public AudioClip gameOverSFX;
    public AudioClip jumpSFX;
    public AudioClip stompSFX;

    [Header("Achievement Settings")]
    public GameObject achievement10;
    public GameObject achievement25;
    public GameObject achievement50;
    public GameObject achievement100;

    [Header("Achievement Audio")]
    public AudioClip audio10;
    public AudioClip audio25;
    public AudioClip audio50;
    public AudioClip audio100;

    private bool unlocked10 = false;
    private bool unlocked25 = false;
    private bool unlocked50 = false;
    private bool unlocked100 = false;
    void Start()
    {
        hudCanvas.SetActive(false);
        if (startScreenUI != null)
        {
            startScreenUI.SetActive(true);
        }
        Time.timeScale = 0f;
        gameOverScreenUI.SetActive(false);

        if (achievement10) achievement10.SetActive(false);
        if (achievement50) achievement50.SetActive(false);
        if (achievement100) achievement100.SetActive(false);
        if (achievement25) achievement25.SetActive(false);
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
        if (backgroundMusicSource != null)
        {
            backgroundMusicSource.Stop();
        }
        PlaySFX(gameOverSFX);

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
    public void PlaySFX(AudioClip clip)
    {
        if (sfxSource != null && clip != null)
        {
            sfxSource.PlayOneShot(clip);
        }
    }
    public void CheckForAchievements()
    {
        int currentScore = ScoreManager.score;
        if (currentScore >= 1 && !unlocked10)
        {
            unlocked10 = true;
            PlaySFX(audio10);
            StartCoroutine(ShowAchievementBriefly(achievement10));
        }
        if (currentScore >= 25 && !unlocked50)
        {
            unlocked50 = true;
            PlaySFX(audio50);
            StartCoroutine(ShowAchievementBriefly(achievement50));
        }
        if (currentScore >= 50 && !unlocked100)
        {
            unlocked100 = true;
            PlaySFX(audio100);
            StartCoroutine(ShowAchievementBriefly(achievement100));
        }
        if (currentScore >= 10 && !unlocked25)
        {
            unlocked25 = true;
            PlaySFX(audio25);
            StartCoroutine(ShowAchievementBriefly(achievement25));
        }
    }
    private IEnumerator ShowAchievementBriefly(GameObject achievement)
    {
     if (achievement != null)
     {
         achievement.SetActive(true);
         yield return new WaitForSecondsRealtime(5f);
         achievement.SetActive(false);
        }
    }
}