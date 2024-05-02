using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DespawnByTime : Despawn
{
    // TODO: Not Finish
    [SerializeField] protected float timer = 0f;
    [SerializeField] protected float delay = 5f;
    protected override void OnEnable()
    {
        base.OnEnable();
        this.ResetTimer();
    }
    protected virtual void ResetTimer()
    {
        this.timer = 0;
    }
    protected override bool canDespawn()
    {
        this.timer += Time.fixedDeltaTime;
        if (this.timer > this.delay) return true;
        return false;
    }
}
