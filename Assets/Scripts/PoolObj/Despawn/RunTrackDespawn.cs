using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunTrackDespawn : DespawnByDistance
{
    public override void DespawnObject()
    {
        RunTrackSpawner.Instance.Despawn(transform.parent);
    }

    protected override void ResetValue()
    {
        base.ResetValue();
        this.disLimit = 300;
    }
}
