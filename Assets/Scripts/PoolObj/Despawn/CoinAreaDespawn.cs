using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinAreaDespawn : DespawnByTime
{
    public override void DespawnObject()
    {
        CoinAreaSpawner.Instance.Despawn(transform.parent);
    }

    protected override void ResetValue()
    {
        base.ResetValue();
        this.delay = 20;
    }
}
