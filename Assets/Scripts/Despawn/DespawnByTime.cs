// using System.Collections;
// using System.Collections.Generic;
using UnityEngine;

public class DespawnByTime : MyMonoBehaviour
{
    [SerializeField] private float aliveTime;

    protected override void OnEnable()
    {
        base.OnEnable();
        Invoke(nameof(Despawn), aliveTime);
    }

    protected virtual void Despawn()
    {
        Destroy(this.gameObject);
    }
}
