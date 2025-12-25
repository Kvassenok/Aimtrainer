using TMPro;
using UnityEngine;

public class EndPanel : MonoBehaviour
{
    [SerializeField] CanvasGroup canvasGroup;
    [SerializeField] TMP_Text highScoreText;

    void OnEnable()
    {
        Timer.OnGameEnded += OnGameEnded;
    }

    void OnDisable()
    {
        Timer.OnGameEnded -= OnGameEnded;
    }

    void OnGameEnded()
    {
        canvasGroup.alpha = 1f;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        highScoreText.text = $"High Score: {ScoreManager.Instance.GetHighScore()}";
    }
}