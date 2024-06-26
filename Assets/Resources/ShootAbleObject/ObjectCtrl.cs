using System;
using System.Collections;
using UIGameDataManager;
using UnityEngine;

public abstract class ObjectCtrl : SaiMonoBehaviour
{
    [Header("Object Ctrl")]
    [SerializeField] protected Transform modle;
    public Transform Modle { get => modle; }
    [SerializeField] protected ShootAbleObjectSO shootAbleObjectSO;
    public ShootAbleObjectSO ShootAbleObjectSO => shootAbleObjectSO;
    [SerializeField] protected ObjectDamageReceiver Receiver;
    public ObjectDamageReceiver ObjectDamageReceiver => Receiver;
    [SerializeField] protected Spawner spawner; 
    public Spawner Spawner => spawner;

    [SerializeField] protected DamageSender damageSender;
    public DamageSender DamageSender { get => damageSender; }
    [SerializeField] protected Despawn despawn;
    public Despawn Despawn { get => despawn; }
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.loadShootAbleObjectSO();
        this.loadModle();
        this.loadReceiver();
        this.LoadSpawner();
        this.loadAnimationDameSender();
        this.loadDespawn();
    }
    protected virtual void loadDespawn()
    {
        if (this.despawn != null) return;
        this.despawn = transform.GetComponentInChildren<Despawn>();
        Debug.Log(gameObject.name + ": loadDespawn" + gameObject);
    }
    protected virtual void loadAnimationDameSender()
    {
        if (this.damageSender != null) return;
        this.damageSender = transform.GetComponentInChildren<DamageSender>();
        Debug.Log(gameObject.name + ": loadDamageSender" + gameObject);
    }
    protected virtual void LoadSpawner()
    {
        if (this.spawner != null) return;
        this.spawner = transform.parent?.parent?.GetComponent<Spawner>();
        Debug.LogWarning(transform.name + ": LoadSpawner", gameObject);
    }
    protected virtual void loadReceiver()
    {
        if (this.Receiver != null) return;
        this.Receiver = transform.GetComponentInChildren<ObjectDamageReceiver>();
        Debug.Log(gameObject.name + ": loadloadReceiver" + gameObject);
    }
    protected virtual void loadModle()
    {
        if (this.modle != null) return;
        this.modle = transform.Find("Modle");
        Debug.Log(gameObject.name + ": loadModle" + gameObject);
    }
    protected virtual void loadShootAbleObjectSO() // ScriptableObject
    {
        if (this.shootAbleObjectSO != null) return;
        string resPath = "ShootAbleObject/" + this.GetObjectTypeString() + "/" + transform.name;
        this.shootAbleObjectSO = Resources.Load<ShootAbleObjectSO>(resPath); //Ph?i t?o Folder l� Resources
        Debug.LogWarning(transform.name + ": LoadShootAbleObjectSO" + resPath, gameObject);
    }
    protected abstract string GetObjectTypeString();
}
