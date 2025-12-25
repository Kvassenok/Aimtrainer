using UnityEngine;

public class SmallTarget : BaseTarget
{
    protected override void Start()
    {
        base.Start();
        size = 0.5f;
        points = 2;
    }
}