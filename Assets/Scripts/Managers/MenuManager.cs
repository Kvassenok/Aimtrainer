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
        float savedSensitivity = PlayerPrefs.GetFloat("MouseSensitivity", 1f);
        sensitivitySlider.value = savedSensitivity;
        sensitivitySlider.onValueChanged.AddListener(OnSensitivityChanged);
        highScoreText.text = $"Рекорд очков: {ScoreManager.Instance.GetHighScore()}";
        UpdateSensitivityText();
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        difficultyPanel.SetActive(false);
    }
    private void OnSensitivityChanged(float value)
    {
        UpdateSensitivityText();
        PlayerPrefs.SetFloat("MouseSensitivity", value);
        PlayerPrefs.Save();  // Сохраняем сразу
    }

    public void ShowDifficultyPanel()
    {
        Debug.Log("Button clicked");
        difficultyPanel.SetActive(true);
    }

    public void SetDifficulty(int diff)
    {
        PlayerPrefs.SetInt("Difficulty", diff);
        StartGame();
    }

    public void StartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("GameScene");
    }

    public void UpdateSensitivityText()
    {
        if (sensitivityValueText != null)
        {
            sensitivityValueText.text = $"Чувствительность: {sensitivitySlider.value.ToString("F1")}";
        }
    }
}