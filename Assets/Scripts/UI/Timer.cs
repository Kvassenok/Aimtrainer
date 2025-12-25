using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    public static Action OnGameEnded;
    public static bool GameEnded { get; private set; }

    [SerializeField] TMP_Text timerText;

    float endTime;
    const float gameTime = 5f;

    public static Timer Instance { get; private set; }  // Новый singleton

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);  // Уничтожаем дубликат
            Debug.Log("Timer duplicate destroyed.");
            return;
        }
        Instance = this;
    }

    void Start()
    {
        GameEnded = false;
        // Удалите OnGameEnded = null; — чтобы не очищать подписчиков каждый раз
        endTime = Time.time + gameTime;
        if (timerText == null)
        {
            Debug.LogError("Timer: timerText is not assigned in Inspector!");
        }
        Debug.Log("Timer Started: GameEnded = " + GameEnded);
    }

    void OnEnable()
    {
        if (SceneManager.GetActiveScene().name == "GameScene")
        {
            GameEnded = false;
            endTime = Time.time + gameTime;
        }
    }

    void Update()
    {
        if (GameEnded || GameManager.IsPaused)
        {
            Debug.Log("Timer Update skipped: GameEnded=" + GameEnded + ", IsPaused=" + GameManager.IsPaused);
            return;
        }

        float timeLeft = endTime - Time.time;

        if (timeLeft <= 0)
        {
            GameEnded = true;
            OnGameEnded?.Invoke();
            ScoreManager.Instance.SaveHighScore();
            timeLeft = 0;
            Debug.Log("Timer: Game ended! Invoking OnGameEnded.");  // Confirm in console
        }

        if (timerText != null)
        {
            timerText.text = $"Время: {timeLeft.ToString("0.0")}";
        }
        else
        {
            Debug.LogWarning("Timer: timerText is null, skipping update.");
        }
    }
}