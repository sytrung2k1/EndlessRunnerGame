using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpDespawn : DespawnByTime
{
    public override void DespawnObject()
    {
        PowerUpSpawner.Instance.Despawn(transform.parent);
    }

    protected override void ResetValue()
    {
        base.ResetValue();
        this.delay = 25;
    }
}
