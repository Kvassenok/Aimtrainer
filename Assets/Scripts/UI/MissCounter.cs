using TMPro;
using UnityEngine;

public class MissCounter : MonoBehaviour
{
    [SerializeField] TMP_Text text;

    void OnEnable()
    {
        TargetShooter.OnTargetMissed += OnTargetMissed;
    }

    void OnDisable()
    {
        TargetShooter.OnTargetMissed -= OnTargetMissed;
    }

    void OnTargetMissed()
    {
        ScoreManager.Instance.AddMiss();
    }

    public void UpdateText(int misses)
    {
        text.text = $"Миссы: {misses}";
    }
}