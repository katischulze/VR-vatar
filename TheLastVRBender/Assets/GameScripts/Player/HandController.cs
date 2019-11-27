using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class HandController : MonoBehaviour
{

    // constants
    private readonly float minDestroyVel = 0.5f;
    private readonly float minCatchVel = 0.5f;
    private readonly float grabTime = 1.0f;
    private readonly float velBoost = 2;
    private readonly float newLifetime = 64.0f;

    // variables
    public GameObject DebugTextLeft;
    public GameObject DebugTextRight;
    public GameObject DebugTextMiddle;

    public GameObject player;
    private GameObject projectile = null;
    private float lastGrabTime;

    private bool right;
    private bool grabWindowActive = false;
    private bool attatched = false;


    // Start is called before the first frame update
    void Start()
    {

        // get if right or left hand
        if (gameObject.name.Contains("Right"))
            right = true;   // what a philosophical statement. right = true. wow.

    }


    IEnumerator DisableCollWithDelay(GameObject theObject)
    {

        yield return new WaitForSeconds(1);
        theObject.GetComponent<Collider>().enabled = false;


    }

    // Update is called once per frame
    void Update()
    {

        //PrintLog("middle", "" + projectile);

        /*if (attatched)
            PrintLog(right ? "right" : "left", "ATTATCHED");
        else
            PrintLog(right ? "right" : "left", "DETATCHED");*/


        // behaviour of a grabbed projectile
        if (attatched)
        {

            if (projectile)
            {
                projectile.GetComponent<BaseProjectile>().friendNotFoe = true;

                // make projectile follow hand
                projectile.GetComponent<Collider>().enabled = false;
                projectile.GetComponent<BaseProjectile>().Freeze();
                projectile.transform.position = gameObject.transform.position;
                //projectile.transform.position = Vector3.MoveTowards(projectile.transform.position, gameObject.transform.position, Time.deltaTime);

                // throw projectile
                if (!Fist())
                {
                    StartCoroutine(DisableCollWithDelay(projectile));
                    projectile.GetComponent<BaseProjectile>().Unfreeze();
                    projectile.GetComponent<BaseProjectile>().MoveToTarget(projectile.transform.position + (right ? OVRInput.GetLocalControllerVelocity(OVRInput.Controller.RTouch) : OVRInput.GetLocalControllerVelocity(OVRInput.Controller.LTouch)));
                    projectile.GetComponent<BaseProjectile>().speed = (right ? OVRInput.GetLocalControllerVelocity(OVRInput.Controller.RTouch).magnitude : OVRInput.GetLocalControllerVelocity(OVRInput.Controller.LTouch).magnitude) * velBoost;
                    projectile.GetComponent<BaseProjectile>().maxLifetime = newLifetime;

                    attatched = false;
                    grabWindowActive = false;
                    projectile = null;
                    //PrintLog("middle", "DETATCHED");

                }
            }

        }


        // the time window in which the player can grab the projectile
        if (grabWindowActive)
        {

            // player has still time to grab
            if (Time.time - lastGrabTime <= grabTime)
            {

                //PrintLog("middle", "GrabTime: " + (Time.time - lastGrabTime));

                if (Fist())
                {

                    if (right)
                        Inputmapper.inst.SetRightVibration(1, 0.1f);
                    else
                        Inputmapper.inst.SetLeftVibration(1, 0.1f);

                    attatched = true;
                    grabWindowActive = false;
                    //PrintLog("middle", "ATTATCHED");

                }

            }

            // if not grabbed after time seconds, player gets hit
            else
            {

                //player.GetComponent<Player>().GetHit();
                //PrintLog("middle", "G HEALTH: " + player.GetComponent<Player>().health);
                grabWindowActive = false;

            }

        }

    }


    // on collision
    private void OnTriggerEnter(Collider other)
    {

        // on collision with projectile
        if (other.name.Contains("Projectile"))
        {

            //PrintLog("middle", "ProjCollision");

            // if fist
            if (Fist())
            {

                // if enough destroy velocity
                if ((right ? OVRInput.GetLocalControllerVelocity(OVRInput.Controller.RTouch).magnitude : OVRInput.GetLocalControllerVelocity(OVRInput.Controller.LTouch).magnitude) >= minDestroyVel)
                {

                    // destroy
                    if (right)
                        Inputmapper.inst.SetRightVibration(1, 0.1f);
                    else
                        Inputmapper.inst.SetLeftVibration(1, 0.1f);
                    Destroy(other.gameObject);
                    //PrintLog("middle", "DESTROYED");

                }

                // if not, player gets hit
                else
                {

                    //player.GetComponent<Player>().GetHit();
                    //PrintLog("middle", "D HEALTH: " + player.GetComponent<Player>().health);

                }

            }

            // if no fist
            else
            {

                // if enough catch velocity
                if ((right ? OVRInput.GetLocalControllerVelocity(OVRInput.Controller.RTouch).magnitude : OVRInput.GetLocalControllerVelocity(OVRInput.Controller.LTouch).magnitude) >= minCatchVel)
                {

                    // player is given grabTime seconds to form a fist
                    if (!attatched && !grabWindowActive)
                    {

                        projectile = other.gameObject;
                        lastGrabTime = Time.time;
                        grabWindowActive = true;

                    }

                    else
                    {
                        // player already has something in this hand or is already trying to grab something. do nothing.
                    }

                }

                // if not, player gets hit
                else
                {

                    //player.GetComponent<Player>().GetHit();
                    //PrintLog("middle", "C HEALTH: " + player.GetComponent<Player>().health);

                }

            }

        }

    }


    // tests if player is forming a grab    (unused)
    private bool Grab()
    {
        if (right)
            return (OVRInput.Get(OVRInput.RawTouch.RIndexTrigger) && (OVRInput.Get(OVRInput.RawTouch.RThumbstick) || OVRInput.Get(OVRInput.RawTouch.A) || OVRInput.Get(OVRInput.RawTouch.B)));
        else
            return (OVRInput.Get(OVRInput.RawTouch.LIndexTrigger) && (OVRInput.Get(OVRInput.RawTouch.LThumbstick) || OVRInput.Get(OVRInput.RawTouch.X) || OVRInput.Get(OVRInput.RawTouch.Y)));
    }


    // tests if player is forming a fist
    private bool Fist()
    {
        if (right)
            return Grab() && OVRInput.Get(OVRInput.RawButton.RHandTrigger);
        else
            return Grab() && OVRInput.Get(OVRInput.RawButton.LHandTrigger);
    }


    // prints a new line in a text canvas   (debug)
    private void PrintLog(string position, string log)
    {
        switch (position)
        {
            case "left":
                {
                    DebugTextLeft.GetComponent<TextMeshPro>().text = log;
                    break;
                }
            case "middle":
                {
                    DebugTextMiddle.GetComponent<TextMeshPro>().text = log;
                    break;
                }
            case "right":
                {
                    DebugTextRight.GetComponent<TextMeshPro>().text = log;
                    break;
                }
            default:
                {
                    DebugTextMiddle.GetComponent<TextMeshPro>().text = "default error";
                    break;
                }
        }
    }

}
