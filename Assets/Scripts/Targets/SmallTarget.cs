using UnityEngine;

public class SmallTarget : BaseTarget
{
    protected override void Start()
    {
        base.Start();
        size = 0.1f;
        points = 2;
    }
}