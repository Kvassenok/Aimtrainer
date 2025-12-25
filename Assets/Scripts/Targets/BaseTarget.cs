using System;
using UnityEngine;

public abstract class BaseTarget : MonoBehaviour
{
    public static Action OnTargetHit;
    public float size = 1f;
    public int points = 1;
    public ParticleSystem hitEffect;

    protected virtual void Start()
    {
        RandomizePosition();
        transform.localScale = new Vector3(size, size, size);
    }

    public virtual void Hit()
    {
        OnTargetHit?.Invoke();
        ScoreManager.Instance.AddScore(points);
        if (hitEffect != null)
        {
            Instantiate(hitEffect, transform.position, Quaternion.identity);
        }
        RandomizePosition();
    }

    protected void RandomizePosition()
    {
        transform.position = TargetBounds.Instance.GetRandomPosition();
    }
}