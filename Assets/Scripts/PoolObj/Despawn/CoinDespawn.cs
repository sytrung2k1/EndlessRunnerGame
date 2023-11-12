using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinDespawn : DespawnByTime
{
    public override void DespawnObject()
    {
        CoinSpawner.Instance.Despawn(transform.parent);
    }

    protected override void ResetValue()
    {
        base.ResetValue();
        this.delay = 15;
    }
}
