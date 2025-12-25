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
    const float gameTime = 15f;

    void Start()
    {
        GameEnded = false;  // Уже есть, но убедись
        OnGameEnded = null;  // Очисти делегат, чтобы избежать дубликатов подписок
        endTime = Time.time + gameTime;
        if (timerText == null)
        {
            Debug.LogError("Timer: timerText is not assigned in Inspector!");
        }
    }
    void OnEnable()  // Или в Awake()
    {
        if (SceneManager.GetActiveScene().name == "GameScene")  // Убедись, что ресет только в игровой сцене
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
            Debug.Log("Timer: Game ended! Invoking OnGameEnded.");
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