using UnityEngine;

public enum Difficulty { Normal, Small }

public class TargetSpawner : MonoBehaviour
{
    public static TargetSpawner Instance;
    public Difficulty currentDifficulty = Difficulty.Normal;
    public GameObject targetPrefab;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        SpawnTargets(5);
    }

    public void SpawnTargets(int count)
    {
        for (int i = 0; i < count; i++)
        {
            GameObject targetObj = Instantiate(targetPrefab, Vector3.zero, Quaternion.identity);
            BaseTarget target = (currentDifficulty == Difficulty.Normal)
                ? targetObj.AddComponent<NormalTarget>()
                : targetObj.AddComponent<SmallTarget>();
        }
    }

    private void OnEnable()
    {
        BaseTarget.OnTargetHit += OnTargetHit;
    }

    private void OnDisable()
    {
        BaseTarget.OnTargetHit -= OnTargetHit;
    }

    private void OnTargetHit()
    {
    }
}