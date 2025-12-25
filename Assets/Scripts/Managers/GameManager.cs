using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance; // Добавлено Singleton для устранения ошибок
    public GameObject pausePanel;
    public Slider sensitivitySlider;
    public static bool IsPaused { get; private set; }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Теперь singleton
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        IsPaused = false;
        pausePanel.SetActive(false);
        sensitivitySlider.value = PlayerPrefs.GetFloat("MouseSensitivity", 1f);
        TargetSpawner.Instance.currentDifficulty = (Difficulty)PlayerPrefs.GetInt("Difficulty", 0);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            IsPaused = !IsPaused;
            pausePanel.SetActive(IsPaused);
            Time.timeScale = IsPaused ? 0 : 1;
            Cursor.visible = IsPaused;
            Cursor.lockState = IsPaused ? CursorLockMode.None : CursorLockMode.Locked;
        }
    }

    public void Resume()
    {
        IsPaused = false;
        pausePanel.SetActive(false);
        Time.timeScale = 1;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void ExitToMenu()
    {
        ScoreManager.Instance.SaveHighScore();
        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1;
    }

    public void ChangeSensitivity(float value)
    {
        PlayerPrefs.SetFloat("MouseSensitivity", value);
        PlayerController player = FindAnyObjectByType<PlayerController>();
        if (player != null) player.mouseSensitivity = value;
    }
}