using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingProjectile : BaseProjectile
{
    public Vector3 rotationVector = Vector3.back;
    public float rotationSpeed = 1.0f;


    public override void Move()
    {
        transform.Rotate(rotationVector.normalized * rotationSpeed * Time.deltaTime);
        base.Move();
    }


}
