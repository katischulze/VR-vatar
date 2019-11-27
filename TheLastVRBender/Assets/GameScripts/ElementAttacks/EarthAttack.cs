using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class EarthAttack : BaseAttack
{
    public GameObject normalRock;
    public GameObject sharpRock;


    public override void StartAttack()
    {
        if (!GameMaster.inst.gameHasStarted) return;
        GameObject newProjectile;
        int r = Random.Range(0, 5);

        if (r == 2)
        {
            newProjectile = Instantiate(sharpRock);
        }
        else
        {
            newProjectile = Instantiate(normalRock);
        }

        newProjectile.transform.SetParent(EnemyManager.inst.projectiles.transform);
        newProjectile.transform.position = transform.position;
        BaseProjectile projectile = newProjectile.GetComponent<BaseProjectile>();

        if (r != 2)
        {
            float d = Vector3.Distance(GameMaster.GetInstance().GetPlayer().transform.position, transform.position);
            projectile.MoveToTarget(GameMaster.GetInstance().GetPlayer().transform.position + Vector3.up);
            projectile.gravity = 3.0f;
            projectile.speed = d * 1.1f;
        }
        else
        {
            float d = Vector3.Distance(GameMaster.GetInstance().GetPlayer().transform.position, transform.position);
            projectile.MoveToTarget(GameMaster.GetInstance().GetPlayer().transform.position + Vector3.up);
            projectile.gravity = 3.0f;
            projectile.speed = d * 2f;
        }

        projectile.transform.localScale *= Random.Range(1.0f, 1.2f);
    }
}
