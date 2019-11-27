using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour
{
    public static Player inst;
    public static int maxHealth = 5;
    public int health = maxHealth;
    private static int regenTime = 10;
    private float healthTimer = regenTime;


    private void Awake()
    {
        inst = this;
    }


    public void GetHit()
    {
        health -= 1;
        if (health <= 0)
        {
            GameMaster.inst.EndGame();
        }
    }


    void Update()
    {
        if (health != maxHealth)
        {
            healthTimer -= Time.deltaTime;
            if (healthTimer <= 0)
            {
                healthTimer = regenTime;
                health += 1;
            }
        }

        if(health <= 0)
        {
            health = 0;
            GameMaster.GetInstance().EndGame();
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Projectile")
        {
            BaseProjectile projectile = other.GetComponent<BaseProjectile>();
            if(projectile != null)
            {
                if (!projectile.friendNotFoe)
                {
                    health -= projectile.damage;
                    projectile.Destroy();
                    //projectile.Explode(Vector3.zero);
                }
            }
        }
    }
}
