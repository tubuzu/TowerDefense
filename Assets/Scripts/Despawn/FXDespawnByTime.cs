// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
using Spawners;

public class FXDespawnByTime : DespawnByTime
{
    protected override void Despawn()
    {
        FXSpawner.Instance.Despawn(this.gameObject);
    }
}
