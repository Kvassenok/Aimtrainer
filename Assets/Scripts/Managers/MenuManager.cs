using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MenuManager : MonoBehaviour
{
    public Slider sensitivitySlider;
    public TMP_Text highScoreText;
    public TMP_Text sensitivityValueText;
    public GameObject difficultyPanel;

    void Start()
    {
        sensitivitySlider.value = PlayerPrefs.GetFloat("MouseSensitivity", 1f);
        highScoreText.text = $"High Score: {ScoreManager.Instance.GetHighScore()}";
        UpdateSensitivityText();
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        difficultyPanel.SetActive(false);
    }

    public void ShowDifficultyPanel()
    {
        difficultyPanel.SetActive(true);
    }

    public void SetDifficulty(int diff)
    {
        PlayerPrefs.SetInt("Difficulty", diff);
        StartGame();
    }

    public void StartGame()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void UpdateSensitivityText()
    {
        if (sensitivityValueText != null)
        {
            sensitivityValueText.text = $"Sensitivity: {sensitivitySlider.value.ToString("F1")}";
        }
    }
}