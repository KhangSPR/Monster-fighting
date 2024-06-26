using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationDameSender : DamageSender
{
    [SerializeField] protected PlayerCtrl playerCtrl;
    [SerializeField] protected EnemyCtrl enemyCtrl;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.loadPlayerCtrl();
        this.loadEnemyCtrl();
    }
    protected virtual void loadPlayerCtrl()
    {
        if (this.playerCtrl != null) return;
        this.playerCtrl = transform.parent.GetComponent<PlayerCtrl>();
        Debug.Log(gameObject.name + ": loadDamageSender" + gameObject);
    }
    protected virtual void loadEnemyCtrl()
    {
        if (this.enemyCtrl != null) return;
        this.enemyCtrl = transform.parent.GetComponent<EnemyCtrl>();
        Debug.Log(gameObject.name + ": loadEnemyCtrl" + gameObject);
    }
    //public override void Send(DamageReceiver receiver)
    //{
    //    base.Send(receiver);
    //    //this.desTroyOBJ();
    //}
    //public virtual void desTroyOBJ()
    //{
    //    this.playerCtrl.Despawn.HidepawnObjChild();
    //}
}
