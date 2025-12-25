using TMPro;
using UnityEngine;

public class ScoreCounter : MonoBehaviour
{
    [SerializeField] TMP_Text text;

    public void UpdateText(int score)
    {
        text.text = $"Счет: {score}";
    }
}