using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InputDebugger : MonoBehaviour {
    public TMP_Text _A;
    public TMP_Text _B;
    public TMP_Text _X;
    public TMP_Text _Y;

    public TMP_Text _LIndexTrigger;
    public TMP_Text _LHandTrigger;

    public TMP_Text _RIndexTrigger;
    public TMP_Text _RHandTrigger;

    public TMP_Text _LThumbstick;
    public TMP_Text _LThumbstickAxis;

    public TMP_Text _RThumbstick;
    public TMP_Text _RThumbstickAxis;

    public TMP_Text _LControllerPosition;
    public TMP_Text _LControllerRotation;
    public TMP_Text _LControllerVelocity;

    public TMP_Text _RControllerPosition;
    public TMP_Text _RControllerRotation;
    public TMP_Text _RControllerVelocity;

    private Transform _CenterCamera;

    //FUNCTIONS===================================
    private void Awake() {
        _CenterCamera = transform.parent;
        transform.rotation = Quaternion.LookRotation(transform.position - _CenterCamera.position);
    }

    void Update() {
        //A Button
        if (OVRInput.Get(OVRInput.RawButton.A)) {
            _A.text = "Button A: PRESSED";
            _A.color = Color.green;
        } else if (OVRInput.Get(OVRInput.RawTouch.A)) {
            _A.text = "Button A: TOUCH";
            _A.color = Color.yellow;
        } else {
            _A.text = "Button A: ";
            _A.color = Color.white;
        }

        //B Button
        if (OVRInput.Get(OVRInput.RawButton.B)) {
            _B.text = "Button B: PRESSED";
            _B.color = Color.green;
        } else if (OVRInput.Get(OVRInput.RawTouch.B)) {
            _B.text = "Button B: TOUCH";
            _B.color = Color.yellow;
        } else {
            _B.text = "Button B: ";
            _B.color = Color.white;
        }

        //X Button
        if (OVRInput.Get(OVRInput.RawButton.X)) {
            _X.text = "Button X: PRESSED";
            _X.color = Color.green;
        } else if (OVRInput.Get(OVRInput.RawTouch.X)) {
            _X.text = "Button X: TOUCH";
            _X.color = Color.yellow;
        } else {
            _X.text = "Button X: ";
            _X.color = Color.white;
        }

        //Y Button
        if (OVRInput.Get(OVRInput.RawButton.Y)) {
            _Y.text = "Button Y: PRESSED";
            _Y.color = Color.green;
        } else if (OVRInput.Get(OVRInput.RawTouch.Y)) {
            _Y.text = "Button Y: TOUCH";
            _Y.color = Color.yellow;
        } else {
            _Y.text = "Button Y: ";
            _Y.color = Color.white;
        }

        //L Index Trigger
        if (OVRInput.Get(OVRInput.RawButton.LIndexTrigger)) {
            _LIndexTrigger.text = "LIndexTrigger: PRESSED";
            _LIndexTrigger.color = Color.green;
        } else if (OVRInput.Get(OVRInput.RawAxis1D.LIndexTrigger) > 0) {
            _LIndexTrigger.text = "LIndexTrigger: " + OVRInput.Get(OVRInput.RawAxis1D.LIndexTrigger).ToString();
            _LIndexTrigger.color = Color.blue;
        } else if (OVRInput.Get(OVRInput.RawTouch.LIndexTrigger)) {
            _LIndexTrigger.text = "LIndexTrigger: TOUCH";
            _LIndexTrigger.color = Color.yellow;
        } else {
            _LIndexTrigger.text = "LIndexTrigger: ";
            _LIndexTrigger.color = Color.white;
        }

        //L Hand Trigger
        if (OVRInput.Get(OVRInput.RawButton.LHandTrigger)) {
            _LHandTrigger.text = "LHandTrigger: PRESSED";
            _LHandTrigger.color = Color.green;
        } else if (OVRInput.Get(OVRInput.RawAxis1D.LHandTrigger) > 0) {
            _LHandTrigger.text = "LHandTrigger: " + OVRInput.Get(OVRInput.RawAxis1D.LHandTrigger).ToString();
            _LHandTrigger.color = Color.blue;
        } else {
            _LHandTrigger.text = "LHandTrigger: ";
            _LHandTrigger.color = Color.white;
        }

        //R Index Trigger
        if (OVRInput.Get(OVRInput.RawButton.RIndexTrigger)) {
            _RIndexTrigger.text = "RIndexTrigger: PRESSED";
            _RIndexTrigger.color = Color.green;
        } else if (OVRInput.Get(OVRInput.RawAxis1D.RIndexTrigger) > 0) {
            _RIndexTrigger.text = "RIndexTrigger: " + OVRInput.Get(OVRInput.RawAxis1D.RIndexTrigger).ToString();
            _RIndexTrigger.color = Color.blue;
        } else if (OVRInput.Get(OVRInput.RawTouch.RIndexTrigger)) {
            _RIndexTrigger.text = "RIndexTrigger: TOUCH";
            _RIndexTrigger.color = Color.yellow;
        } else {
            _RIndexTrigger.text = "RIndexTrigger: ";
            _RIndexTrigger.color = Color.white;
        }

        //R Hand Trigger
        if (OVRInput.Get(OVRInput.RawButton.RHandTrigger)) {
            _RHandTrigger.text = "RHandTrigger: PRESSED";
            _RHandTrigger.color = Color.green;
        } else if (OVRInput.Get(OVRInput.RawAxis1D.RHandTrigger) > 0) {
            _RHandTrigger.text = "RHandTrigger: " + OVRInput.Get(OVRInput.RawAxis1D.RHandTrigger).ToString();
            _RHandTrigger.color = Color.blue;
        } else {
            _RHandTrigger.text = "RHandTrigger: ";
            _RHandTrigger.color = Color.white;
        }

        //L Thumbstick
        if (OVRInput.Get(OVRInput.RawButton.LThumbstick)) {
            _LThumbstick.text = "LThumbstick: PRESSED";
            _LThumbstick.color = Color.green;
        } else if (OVRInput.Get(OVRInput.RawTouch.LThumbstick)) {
            _LThumbstick.text = "LThumbstick: TOUCH";
            _LThumbstick.color = Color.yellow;
        } else {
            _LThumbstick.text = "LThumbstick: ";
            _LThumbstick.color = Color.white;
        }

        //L Thumbstick Axis
        if (OVRInput.Get(OVRInput.RawAxis2D.LThumbstick).magnitude > 0.001) {
            _LThumbstickAxis.text = "LThumbstick: " + OVRInput.Get(OVRInput.RawAxis2D.LThumbstick).ToString();
            _LThumbstickAxis.color = Color.blue;
        } else {
            _LThumbstickAxis.text = "LThumbstick: RESTED";
            _LThumbstickAxis.color = Color.white;
        }

        //R Thumbstick
        if (OVRInput.Get(OVRInput.RawButton.RThumbstick)) {
            _RThumbstick.text = "RThumbstick: PRESSED";
            _RThumbstick.color = Color.green;
        } else if (OVRInput.Get(OVRInput.RawTouch.RThumbstick)) {
            _RThumbstick.text = "RThumbstick: TOUCH";
            _RThumbstick.color = Color.yellow;
        } else {
            _RThumbstick.text = "RThumbstick: ";
            _RThumbstick.color = Color.white;
        }

        //R Thumbstick Axis
        if (OVRInput.Get(OVRInput.RawAxis2D.RThumbstick).magnitude > 0.001) {
            _RThumbstickAxis.text = "RThumbstick: " + OVRInput.Get(OVRInput.RawAxis2D.RThumbstick).ToString();
            _RThumbstickAxis.color = Color.blue;
        } else {
            _RThumbstickAxis.text = "RThumbstick: RESTED";
            _RThumbstickAxis.color = Color.white;
        }

        //L Controller
        _LControllerPosition.text = "LPosition: " + OVRInput.GetLocalControllerPosition(OVRInput.Controller.LTouch).ToString();
        _LControllerRotation.text = "LRotation: " + OVRInput.GetLocalControllerRotation(OVRInput.Controller.LTouch).ToString();
        _LControllerVelocity.text = "LVelocity: " + OVRInput.GetLocalControllerRotation(OVRInput.Controller.LTouch).ToString();

        //R Controller
        _RControllerPosition.text = "RPosition: " + OVRInput.GetLocalControllerPosition(OVRInput.Controller.RTouch).ToString();
        _RControllerRotation.text = "RRotation: " + OVRInput.GetLocalControllerRotation(OVRInput.Controller.RTouch).ToString();
        _RControllerVelocity.text = "RVelocity: " + OVRInput.GetLocalControllerVelocity(OVRInput.Controller.RTouch).ToString();
    }
}
