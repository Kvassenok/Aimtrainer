using System.IO;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;
    private int currentScore = 0;
    private int highScore = 0;
    private int misses = 0;
    private string highScoreFile;
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        highScoreFile = Application.persistentDataPath + "/highscore.txt";
        LoadHighScore();
    }

    public void AddScore(int points)
    {
        currentScore += points;
        if (currentScore > highScore) highScore = currentScore;
        UpdateScoreUI();
    }

    public void AddMiss()
    {
        misses++;
        UpdateMissUI();
    }

    public void ResetScore()
    {
        currentScore = 0;
        misses = 0;
        UpdateScoreUI();
        UpdateMissUI();
    }

    public void SaveHighScore()
    {
        File.WriteAllText(highScoreFile, highScore.ToString());
    }

    private void LoadHighScore()
    {
        if (File.Exists(highScoreFile))
        {
            highScore = int.Parse(File.ReadAllText(highScoreFile));
        }
    }

    private void UpdateScoreUI()
    {
        ScoreCounter scoreCounter = FindAnyObjectByType<ScoreCounter>();
        if (scoreCounter != null) scoreCounter.UpdateText(currentScore);
    }

    private void UpdateMissUI()
    {
        MissCounter missCounter = FindAnyObjectByType<MissCounter>();
        if (missCounter != null) missCounter.UpdateText(misses);
    }

    public int GetCurrentScore() { return currentScore; }
    public int GetHighScore() { return highScore; }
    public int GetMisses() { return misses; }
}