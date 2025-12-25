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

    void Awake()
    {
        if (canvasGroup != null)
        {
            canvasGroup.alpha = 0f;
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
        }
        Cursor.visible = false;  // Если нужно для геймплея
        Cursor.lockState = CursorLockMode.Locked;
    }
    void OnGameEnded()
    {
        if (canvasGroup == null || highScoreText == null)
        {
            Debug.LogError("EndPanel: canvasGroup or highScoreText is null! Assign in Inspector.");
            return;
        }
        canvasGroup.alpha = 1f;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        highScoreText.text = $"High Score: {ScoreManager.Instance.GetHighScore()}";
    }
}