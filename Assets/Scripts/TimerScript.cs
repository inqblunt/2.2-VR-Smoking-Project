using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerScript : MonoBehaviour {

    public static bool firstIsStarted = false;
    public static bool secondIsStarted = false;
    public static bool buttonEnabled = true;
    public static float timer = 0;
    public Text timeText;

	// Use this for initialization
	void Start () {
		// StartTimer();
	}

    public static void StartTimer ()
    {
        // player starts building phase
        firstIsStarted = true;
        buttonEnabled = false; // disables starting button 
    }
	
	// Update is called once per frame
	void Update () {
		if (firstIsStarted == true || secondIsStarted == true)
        {
            timer += Time.deltaTime;
        }
        if (timer >= 300 && secondIsStarted != true)
        {
            firstIsStarted = false;
            timer = 0;
            // start second round, enable spawning and AI patterns
            secondIsStarted = true;
        }
        if (timer >= 120 && secondIsStarted == true)
        {
            secondIsStarted = false;
            timer = 0;
            //end game
            //display score and cleanup
            //reset game
        }
        var minutes = timer / 60; //Divide the guiTime by sixty to get the minutes.
        var seconds = timer % 60;//Use the euclidean division for the seconds.
        var fraction = (timer * 100) % 100;

        //update the label value
        timeText.text = string.Format("{0:00} : {1:00} : {2:000}", minutes, seconds, fraction);
    }


}
