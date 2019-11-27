using System;
using System.Collections;
using System.Collections.Generic;
using GameScripts;
using UnityEngine;

public class LaserController : MonoBehaviour
{
    private bool rightHand = true;
    
    
    // Start is called before the first frame update
    void Start()
    {
        if (gameObject.transform.parent.name.Contains("Left"))
        {
            rightHand = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        /*if (true || rightHand && OVRInput.Get(OVRInput.RawButton.RIndexTrigger) || 
            !rightHand && OVRInput.Get(OVRInput.RawButton.LIndexTrigger))
        {
            if (UIMainMenu.inst.gameObject.activeSelf)
            {
                //Debug.DrawRay(transform.position, Vector3.forward * Mathf.Infinity);
                if (Physics.Raycast(transform.position, Vector3.forward, out RaycastHit hit, 50,
                    (int) Math.Pow(2, 9)))
                {
                    print("hit " + hit.collider.transform.parent.gameObject.name);
                    UIMainMenu.inst.PressButton(hit.collider.transform.parent.gameObject.name);
                }
            }
        }*/
    }
}
