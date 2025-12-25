using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static bool IsPaused { get; private set; }

    [SerializeField] private CanvasGroup pausePanel;

    void Awake()
    {
        IsPaused = false;
        if (pausePanel != null)
        {
            pausePanel.alpha = 0f;
            pausePanel.interactable = false;
            pausePanel.blocksRaycasts = false;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    public void TogglePause()
    {
        IsPaused = !IsPaused;
        Time.timeScale = IsPaused ? 0f : 1f;
        if (pausePanel != null)
        {
            pausePanel.alpha = IsPaused ? 1f : 0f;
            pausePanel.interactable = IsPaused;
            pausePanel.blocksRaycasts = IsPaused;
        }
        Cursor.visible = IsPaused;
        Cursor.lockState = IsPaused ? CursorLockMode.None : CursorLockMode.Locked;
    }

    public void ExitToMenu()
    {
        ScoreManager.Instance.SaveHighScore();
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    public void RestartGame()
    {
        Timer.GameEnded = false;
        ScoreManager.Instance.ResetScore();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}