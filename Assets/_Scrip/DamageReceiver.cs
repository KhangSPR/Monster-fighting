using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DamageReceiver : SaiMonoBehaviour
{
    [Header("Damage Receiver")]
    [SerializeField] protected int isHP = 1;
    [SerializeField] protected int isMaxHP = 3;
    [SerializeField] protected float exitTimeTwitch = 1f;
    [SerializeField] protected float exitTimeBurn = 2f;
    [SerializeField] protected int DamagePerSecondFire = 2;
    [SerializeField] protected int DamagePerSecondtwitch = 2;
    public bool isDead = false;

    public int IsHP => isHP;
    public int IsMaxHP => isMaxHP;
    protected override void OnEnable() // Goi 1 lan moi khi reset
    {
        this.ReBorn();
    }
    protected override void loadValue()
    {
        base.loadValue();
        this.ReBorn();
    }
    public virtual void ReBorn()
    {
        this.isHP = this.isMaxHP;
        this.isDead = false;
        this._IsBurning = false;
    }
    protected virtual void Add(int ADD)
    {
        if (this.isDead == true) return;
        this.isHP += ADD;
        if (this.isHP > this.isMaxHP) this.isHP = this.isMaxHP;
    }
    public virtual void deDuct(int Deduct)
    {
        if (this.isDead == true) return;
        this.isHP -= Deduct;
        if (this.isMaxHP < 0)
            this.isHP = 0;
        this.checkDead();
    }
    public virtual bool LoseHealth(int Deduct)
    {
        //health = health - amount
        this.isHP -= Deduct;

        if (this.isHP <= 0)
        {
            this.checkDead();
            return true;
        }
        return false;
    }

    public virtual bool IsDead()
    {
        return this.isHP <= 0;
    }
    protected virtual void checkDead()
    {
        if (!this.IsDead()) return;
        this.isDead = true;
        this.onDead();
    }
    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.parent.tag == "Fire")
        {
            StartBurning(DamagePerSecondFire);
        }
        if (collision.transform.parent.tag == "Electric")
        {
            StartTWitch(DamagePerSecondtwitch);
        }

    }
    protected void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.parent.tag == "Fire")
        {
            //đếm thời gian 5 giây
            StopCoroutine(CountDownAndStopBurning(exitTimeBurn));
        }
        if (collision.transform.parent.tag == "Electric")
        {
            //đếm thời gian 5 giây
            StopCoroutine(CountDownAndStopTWitch(exitTimeTwitch));
        }
    }
    // IS ELECTRIC _ DAME RECEIVER ELECTRIC
    [SerializeField] private bool _IsTWitch;
    public bool IsTWitch { get => _IsTWitch; set => _IsTWitch = value; }
    private Coroutine TWitchoroutine;
    public void StartTWitch(int DamagePerSecond)
    {
        IsTWitch = true;
        if (TWitchoroutine != null)
        {
            StopCoroutine(TWitchoroutine);
        }
        TWitchoroutine = StartCoroutine(TWitch(DamagePerSecond));
    }
    private IEnumerator TWitch(int DamagePerSecond)
    {
        float minTimeToDamage = 1f / DamagePerSecond;
        WaitForSeconds wait = new WaitForSeconds(minTimeToDamage);
        int damagePerTick = Mathf.FloorToInt(minTimeToDamage) + 1;

        // Trừ dame
        deDuct(damagePerTick);
        while (IsTWitch)
        {
            yield return wait;
            // Trừ dame
            deDuct(damagePerTick);
        }
    }
    private IEnumerator CountDownAndStopTWitch(float time)
    {
        yield return new WaitForSeconds(time);
        StopTWitch();
    }
    public void StopTWitch()
    {
        _IsTWitch = false;
        if (TWitchoroutine != null)
        {
            StopCoroutine(TWitchoroutine);
        }
    }
    // IS FIRE _ DAME RECEIVER FIRE
    [SerializeField] private bool _IsBurning;
    public bool IsBurning { get => _IsBurning; set => _IsBurning = value; }
    private Coroutine Burncoroutine;
    ////hàm này đặt ở enemy
    private IEnumerator Burn(int DamagePerSecond)
    {
        float minTimeToDamage = 1f / DamagePerSecond;
        WaitForSeconds wait = new WaitForSeconds(minTimeToDamage);
        int damagePerTick = Mathf.FloorToInt(minTimeToDamage) + 1;

        // Trừ dame
        deDuct(damagePerTick);
        while (IsBurning)
        {
            yield return wait;
            // Trừ dame
            deDuct(damagePerTick);
        }
    }
    private IEnumerator CountDownAndStopBurning(float time)
    {
        yield return new WaitForSeconds(time);
        StopBurning();
    }
    public void StartBurning(int DamagePerSecond)
    {
        IsBurning = true;
        if (Burncoroutine != null)
        {
            StopCoroutine(Burncoroutine);
        }
        Burncoroutine = StartCoroutine(Burn(DamagePerSecond));
    }

    public void StopBurning()
    {
        _IsBurning = false;
        if (Burncoroutine != null)
        {
            StopCoroutine(Burncoroutine);
        }
    }
    public abstract void onDead();
}
