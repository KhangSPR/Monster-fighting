using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDamageReceiver : DamageReceiver
{
    [Header("ShootAbleObject")]
    [SerializeField] protected ObjectCtrl shootAbleObjectCtrl;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.loadjunkctrl();
    }
    protected virtual void loadjunkctrl()
    {
        if (this.shootAbleObjectCtrl != null) return;
        this.shootAbleObjectCtrl = transform.parent.GetComponent<ObjectCtrl>();
        Debug.Log(gameObject.name + ": loadjunkctrl" + gameObject);
    }
    public override void onDead()
    {
        this.shootAbleObjectCtrl.Despawn.deSpawnObjParent();
        // Tiếp tục thực hiện các hành động khác sau khi dừng coroutine và deSpawnObj
    }
    public override bool IsDead()
    {
        return base.IsDead();
    }
    //protected virtual void ondeaddrop()
    //{
    //    Vector3 pos = transform.position;
    //    Quaternion rot = transform.rotation;
    //    ItemDropSpawner.Instance.Drop(this.shootAbleObjectCtrl.ShootAbleObjectSO.dropList, pos, rot);
    //}

    //protected virtual void ondeadfx()
    //{
    //    string fxname = this.getondeadfxname();
    //    Transform fxondead = FXSpawner.Instance.Spawn(fxname, transform.position, transform.rotation); //ham smoke bang ten
    //    fxondead.gameObject.SetActive(true);
    //}
    //protected virtual string getondeadfxname()
    //{
    //    return FXSpawner.SmokeOne;
    //}
    public override void ReBorn()
    {
        this.isMaxHP = this.shootAbleObjectCtrl.ShootAbleObjectSO.hpMax;
        base.ReBorn();
    }
}
