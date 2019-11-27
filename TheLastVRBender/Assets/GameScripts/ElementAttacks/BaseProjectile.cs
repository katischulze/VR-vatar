using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BaseProjectile : MonoBehaviour
{
    public GameObject[] explosionParticlesPrefabs = new GameObject[1];

    public float speed = 0.1f;
    public float delay = 0.0f;
    public Vector3 offset = Vector3.zero;

    public float gravity = 2.0f;

    public bool friendNotFoe = false;
    public int damage = 1;

    public float maxLifetime = 64.0f;

    private Vector3 gravityVector = Vector3.zero;
    private Vector3 movementVector;

    private bool moving;
    private bool frozen;


    public void Start()
    {
        frozen = false;
        //gravityVector = Vector3.zero;
    }


    public void Update()
    {
        if(delay <= 0.0f)
        {
            if (moving)
            {
                Move();
            }
        }
        else
        {
            delay -= Time.deltaTime;
        }

        if (!frozen)
        {
            maxLifetime -= Time.deltaTime;
        }

        if(maxLifetime <= 0.0f)
        {
            Destroy(gameObject);
        }
    }


    public void Spawn(Vector3 pos)
    {
        transform.position = pos + offset;
    }


    public void MoveToTarget(Vector3 targetPos)
    {
        movementVector = targetPos - transform.position;
        movementVector = movementVector.normalized;
        gravityVector = Vector3.zero;

        //Debug.Log(">>> " + movementVector);

        moving = true;
    }


    public float GetSpeed()
    {
        return speed;
    }


    public void SetSpeed(float speed)
    {
        this.speed = speed;
    }


    public virtual void Move()
    {
        if (!frozen)
        {
            transform.position += (movementVector * speed + gravityVector) * Time.deltaTime;
            gravityVector += Vector3.down * 0.01f * gravity;
        }
    }


    /**
     * Animate???
     */
    public void Destroy()
    {
        Destroy(gameObject);
    }


    public void Explode(Vector3 particleDirection)
    {
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<Collider>().enabled = false;

        foreach(GameObject explosionParticlesPrefab in explosionParticlesPrefabs)
        {
            GameObject particles = Instantiate(explosionParticlesPrefab);
            particles.transform.SetParent(transform);
            particles.transform.Rotate(particleDirection);
            particles.transform.position = transform.position;
            particles.GetComponent<ParticleSystem>().Play();
        }

        StartCoroutine(MarkedToDie());

    }


    IEnumerator MarkedToDie()
    {
        yield return new WaitForSeconds(1.2f);
        //Destroy(transform.GetChild(0).gameObject);
        Destroy(gameObject);
    }


    public void Freeze()
    {
        frozen = true;
    }


    public void Unfreeze()
    {
        frozen = false;
    }


    public bool IsFrozen()
    {
        return frozen;
    }
}