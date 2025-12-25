using System;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public static Action OnGameEnded;
    public static bool GameEnded { get; private set; }

    [SerializeField] TMP_Text timerText;

    float endTime;
    const float gameTime = 60f;

    void Start()
    {
        GameEnded = false;
        endTime = Time.time + gameTime;
        if (timerText == null)
        {
            Debug.LogError("Timer: timerText is not assigned in Inspector!");
        }
    }

    void Update()
    {
        if (GameEnded || GameManager.IsPaused) return;

        float timeLeft = endTime - Time.time;

        if (timeLeft <= 0)
        {
            GameEnded = true;
            OnGameEnded?.Invoke();
            ScoreManager.Instance.SaveHighScore();
            timeLeft = 0;
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