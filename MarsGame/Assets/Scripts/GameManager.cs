using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public GameObject startScreenUI;
    public PlayerController playerController;
    void Start()
    {
        if (startScreenUI != null)
        {
            startScreenUI.SetActive(true);
        }
        Time.timeScale = 0f;
    }
    public void StartGame()
    {
        startScreenUI.SetActive(false);

        Time.timeScale = 1f;

        ScoreManager.score = 0;

    }
}