using UnityEngine;

public abstract class BaseAbstract : SaiMonoBehaviour
{
    [Header("Enemy Abtract")]
    [SerializeField] protected PortalSpawnManager baseCtrl;
    public PortalSpawnManager BaseCtrl { get => baseCtrl; }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadPlayerCtrl();
    }
    protected virtual void LoadPlayerCtrl()
    {
        if (this.baseCtrl != null) return;
        this.baseCtrl = transform.parent.GetComponent<PortalSpawnManager>();
        Debug.Log(transform.name + ": LoadPlayerCtrl", gameObject);
    }
}
