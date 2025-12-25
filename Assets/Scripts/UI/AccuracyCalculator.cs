using TMPro;
using UnityEngine;

public class AccuracyCalculator : MonoBehaviour
{
    [SerializeField] TMP_Text accuracyText;

    void OnEnable()
    {
        Timer.OnGameEnded += CalculateAccuracy;
    }

    void OnDisable()
    {
        Timer.OnGameEnded -= CalculateAccuracy;
    }

    void CalculateAccuracy()
    {
        int score = ScoreManager.Instance.GetCurrentScore();
        int misses = ScoreManager.Instance.GetMisses();
        float accuracy = (score + misses) > 0 ? (float)score / (float)(score + misses) * 100f : 0f;
        accuracyText.text = $"Accuracy: {accuracy.ToString("0")}%";
    }
}