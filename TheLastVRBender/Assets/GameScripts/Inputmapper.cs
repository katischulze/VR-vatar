using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Inputmapper : MonoBehaviour{
    public static Inputmapper inst;
    private float rTime;
    private float lTime;
    private float rAmpl;
    private float lAmpl;
 
    //FUNCTIONS===================================
    void Update() {
        LeftVibration();
        RightVibration();
    }

    private void Awake() {
        inst = this;
    }

    public void SetRightVibration(float amplitude, float time) {
        rTime = time;
        rAmpl = amplitude;
    }

    public void SetLeftVibration(float amplitude, float time) {
        lTime = time;
        lAmpl = amplitude;
    }

    private void RightVibration() {
        if (rTime > 0) {
            rTime -= Time.deltaTime;

            if (rTime > 0) {
                OVRInput.SetControllerVibration(0f, rAmpl, OVRInput.Controller.RTouch);
            } else {
                OVRInput.SetControllerVibration(0f, 0f, OVRInput.Controller.RTouch);
            }
        }
    }

    private void LeftVibration() {
        if (lTime > 0) {
            lTime -= Time.deltaTime;

            if (lTime > 0) {
                OVRInput.SetControllerVibration(0f, lAmpl, OVRInput.Controller.LTouch);
            } else {
                OVRInput.SetControllerVibration(0f, 0f, OVRInput.Controller.LTouch);
            }
        }
    }

}
