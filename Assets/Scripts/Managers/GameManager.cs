using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static bool IsPaused { get; private set; }

    private CanvasGroup pausePanel;  // Удалите [SerializeField]

    void Awake()
    {
        pausePanel = GameObject.Find("pausePanel").GetComponent<CanvasGroup>();  // Находим по имени
        if (pausePanel == null)
        {
            Debug.LogError("GameManager: pausePanel not found! Check name or add CanvasGroup.");
            return;
        }

        IsPaused = false;
        Time.timeScale = 1f;
        pausePanel.alpha = 0f;
        pausePanel.interactable = false;
        pausePanel.blocksRaycasts = false;
        Debug.Log("PausePanel hidden on Awake.");
    }

    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        Debug.Log("GameManager Start: Cursor locked, ready for input.");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
            Debug.Log("Escape pressed, toggling pause.");
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
        ScoreManager.Instance.ResetScore();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}