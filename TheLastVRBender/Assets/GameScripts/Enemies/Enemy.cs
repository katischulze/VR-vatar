using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class Enemy : MonoBehaviour
{
    private List<BaseAttack> attacks = new List<BaseAttack>();

    public Elements element;


    public void AddAttack(BaseAttack attack)
    {
        attacks.Add(attack);
    }


    public BaseAttack[] GetAttacks()
    {
        return attacks.ToArray();
    }


    /**
     * Enemy Attacks
     */
    public virtual void Attack()
    {
        foreach(BaseAttack attack in attacks)
        {
            attack.StartAttack();
        }
    }


    /**
     * True if ready to attack
     */
    public abstract bool IsReadyToAttack();
}
