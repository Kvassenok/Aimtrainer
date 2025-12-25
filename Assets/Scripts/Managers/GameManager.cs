using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static bool IsPaused { get; private set; }

    private CanvasGroup pausePanel;

    void Awake()
    {
        // Попробуем найти по тегу (добавьте тег "PausePanel" к объекту в Inspector > Tag)
        GameObject panelObj = GameObject.FindWithTag("PausePanel");
        if (panelObj == null)
        {
            // Альтернатива по имени
            panelObj = GameObject.Find("pausePanel");  // Или "Canvas/pausePanel"
        }

        if (panelObj != null)
        {
            pausePanel = panelObj.GetComponent<CanvasGroup>();
        }

        if (pausePanel == null)
        {
            Debug.LogError("GameManager: pausePanel not found! Check name, tag or add CanvasGroup.");
            return;
        }

        // Активируем объект, если он был выключен
        panelObj.SetActive(true);

        IsPaused = false;
        Time.timeScale = 1f;
        pausePanel.alpha = 0f;
        pausePanel.interactable = false;
        pausePanel.blocksRaycasts = false;
        Debug.Log("PausePanel found, activated and hidden on Awake.");
    }

    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        Debug.Log("GameManager Start: Cursor locked, ready for input.");
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
            Debug.Log("TogglePause: IsPaused=" + IsPaused + ", Alpha set to " + pausePanel.alpha + ", Panel active=" + pausePanel.gameObject.activeSelf);
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