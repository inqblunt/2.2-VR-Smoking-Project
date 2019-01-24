using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SampleVRTeleporterController : MonoBehaviour {

    public VRTeleporter teleporter;
    bool isPressed = false;

	void Update () {

        if (OVRInput.Get(OVRInput.Button.SecondaryThumbstick, OVRInput.Controller.Touch) == true && !isPressed)
        {
            teleporter.ToggleDisplay(true);
            isPressed = true;
        }

        if(OVRInput.Get(OVRInput.Button.SecondaryThumbstick, OVRInput.Controller.Touch) == false && isPressed)
        {
            teleporter.Teleport();
            teleporter.ToggleDisplay(false);
            isPressed = false;
        }
	}
}
