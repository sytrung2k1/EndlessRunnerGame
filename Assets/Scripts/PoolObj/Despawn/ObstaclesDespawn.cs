using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstaclesDespawn : DespawnByTime
{
    public override void DespawnObject()
    {
        ObstaclesSpawner.Instance.Despawn(transform.parent);
    }

    protected override void ResetValue()
    {
        base.ResetValue();
        this.delay = 25;
    }
}
