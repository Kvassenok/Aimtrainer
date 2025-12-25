using UnityEngine;

public class NormalTarget : BaseTarget
{
    protected override void Start()
    {
        base.Start();
        size = 1f;
        points = 1;
    }
}