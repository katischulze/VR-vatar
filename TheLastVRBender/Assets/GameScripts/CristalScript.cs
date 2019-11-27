using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CristalScript : MonoBehaviour
{
    public GameObject destroyedCristal;

    private void OnTriggerEnter(Collider other)
    {
        Instantiate(destroyedCristal);
        destroyedCristal.transform.SetParent(transform.parent);
        destroyedCristal.transform.rotation = transform.rotation;
        destroyedCristal.transform.position = transform.position;
        Destroy(gameObject);
    }
}
