using System;
using UnityEngine;

public class TargetShooter : MonoBehaviour
{
    public static Action OnTargetMissed;
    [SerializeField] Camera cam;

    void Update()
    {
        if (Timer.GameEnded || GameManager.IsPaused) return;

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f));
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                BaseTarget target = hit.collider.gameObject.GetComponent<BaseTarget>();
                if (target != null)
                {
                    target.Hit();
                }
                else
                {
                    OnTargetMissed?.Invoke();
                }
            }
            else
            {
                OnTargetMissed?.Invoke();
            }
        }
    }
}