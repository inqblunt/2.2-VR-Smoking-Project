using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokeColl : MonoBehaviour {

    // Use this for initialization
    private void OnParticleCollision(GameObject other)
    {
        if (other.tag == "nosmoke")
        {
            TimerScript.score -= 100;
            Debug.Log("Hit");
        }
    }
}
