using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ChargeEnemy : BaseEnemy
{
    /* Enemy Charge time for attack */
    public float chargeTime = 1.5f;


    private float chargeTimer = 0.0f;


    public new void Update()
    {
        if (IsCharging())
        {
            //Debug.Log(name + ": Charging...");
            chargeTimer -= Time.deltaTime;
            if (chargeTimer <= 0.0f)
            {
                chargeTimer = 0.0f;
                base.Attack();
            }
        }

        base.Update();
    }


    public bool IsCharging()
    {
        return chargeTimer > 0.0f;
    }


    public override void Attack()
    {
        if (!IsCharging()) {
            chargeTimer = chargeTime;
        }
    }


    public new bool IsReadyToAttack()
    {
        return !IsCharging() && base.IsReadyToAttack();
    }

}