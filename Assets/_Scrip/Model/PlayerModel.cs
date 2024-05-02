using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerModel : AbstractModel
{
    protected override void AnimationLoading()
    {
        bool shouldAttack = false;

        switch (currentState)
        {
            case State.Idle:
                PlayAnimation("Attack", false);
                isAttacking = false;
                break;
            case State.Attack:
                if (!isAttacking && currentDelay <=0)
                {
                    PlayAnimation("Attack", true);
                    isAttacking = true;
                    isAnimationComplete = false;
                }
                break;
        }

        if (this.playerCtrl.PlayerAttack.canAttack)
        {
            shouldAttack = true;
        }

        if (shouldAttack)
        {
            currentState = State.Attack;
        }
        else
        {
            currentState = State.Idle;
        }

        if (isAttacking && isAnimationComplete)
        {
            this.SpawnBullet();
            isAnimationComplete = false;
            currentDelay = delayAttack; // Set time Wait
            currentState = State.Idle; // Next State Idle

            Debug.Log(isAnimationComplete);

        }
    }

    protected override void SpawnBullet()
    {
        if(transform.gameObject.tag =="BulletDefaut")
        {
            this.playerCtrl.PlayerShooter.Shoot();
        }
        else if(transform.gameObject.tag =="BulletPX")
        {
            this.playerCtrl.PlayerShooter.ShootPX();
        }
        else
        {

        }    
    }
}
