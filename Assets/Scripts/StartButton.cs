using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartButton : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown("space"))
        {
            TimerScript.StartTimer();
        }
	}

    /*private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "starter")
        {
            TimerScript.StartTimer();
        }

    }*/
}
