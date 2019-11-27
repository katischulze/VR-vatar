using UnityEngine;


public class TestAttack : BaseAttack
{
    /* The projectile that should be fired, e.g. a rock */
    public GameObject ProjectilePref;


    public override void StartAttack()
    {
        if (GameMaster.inst.gameHasStarted)
        {
            GameObject newProjectile = Instantiate(ProjectilePref, transform);
            newProjectile.transform.SetParent(EnemyManager.inst.projectiles.transform);
            newProjectile.transform.position = transform.position;
            //Rigidbody newRb = newProjectile.GetComponent<Rigidbody>();
            //newRb.velocity = Vector3.back * 5;
            BaseProjectile projectile = newProjectile.GetComponent<BaseProjectile>();
            projectile.MoveToTarget(GameMaster.GetInstance().GetPlayer().transform.position);
        }
    }
}