using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BaseEnemy : Enemy
{   
    /* Cooldown between attacks */
    public float cooldown = 3.0f;
    /* The element Attacks of this enemy */
    public BaseAttack[] elementAttacks = new BaseAttack[0];
    public int hp = 100;


    private Rigidbody body;
    private float cooldownTimer = 3.0f;


    public void Start()
    {
        body = GetComponent<Rigidbody>();

        foreach(BaseAttack baseAttack in elementAttacks)
        {
            AddAttack(baseAttack);
        }
    }


    public void Update()
    {
        if(cooldownTimer > 0.0f)
        {
            cooldownTimer -= Time.deltaTime;
        }

        if(hp <= 0)
        {
            Kill();
        }
    }


    public override void Attack()
    {
        if (!IsReadyToAttack())
        {
            return;
        }

        Debug.Log(">> " + name + ": Attacking!!!!!");

        cooldownTimer = cooldown;
        base.Attack();
    }

    
    public override bool IsReadyToAttack()
    {
        if(cooldownTimer <= 0.0f)
        {
            return true;
        }
        return false;
    }


    public void Kill()
    {
        hp = 0;

        // Animate ???
        GameMaster.kills += 1;
        EnemyManager.inst.enemies.Remove(this);
        Destroy(gameObject);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Projectile")
        {
            BaseProjectile projectile = other.GetComponent<BaseProjectile>();
            if (projectile != null)
            {
                if (projectile.friendNotFoe)
                {
                    hp -= projectile.damage;
                    projectile.Destroy();
                }
            }
        }
    }
}