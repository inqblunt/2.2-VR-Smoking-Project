﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokingAreasScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.tag == "smoke")
        {
            TimerScript.isSmoking--;
            SmokeMovementScript.inBox = true;
            Debug.Log("smokeinbox");
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.tag == "smoke")
        {
            TimerScript.isSmoking--;
            SmokeMovementScript.inBox = false;
        }
    }
}
