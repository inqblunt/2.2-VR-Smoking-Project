using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WardenScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        transform.Find("NPC").gameObject.SetActive(false);
        transform.GetComponent<OVRGrabbable>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (TimerScript.secondIsStarted == true)
        {
            transform.Find("NPC").gameObject.SetActive(true);
            transform.GetComponent<OVRGrabbable>().enabled = true;
        }
        else
        {
            transform.Find("NPC").gameObject.SetActive(false);
            transform.GetComponent<OVRGrabbable>().enabled = false;
        }
    }

 
}
