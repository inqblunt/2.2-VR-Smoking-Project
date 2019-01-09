using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NosmokeMovementScript : MonoBehaviour {

    public NavMeshAgent nosmoke;
    public static int smokeStack = 0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (TimerScript.secondIsStarted == true)
        {
            GameObject target = FindClosestSmArea();
            Transform targetTransform = target.GetComponent<Transform>();
            nosmoke.SetDestination(targetTransform.position);
        }
        if (smokeStack >= 5)
        {
            SpawnScript.nosmokesDestroyed++;
            Destroy(gameObject);
        }
	}

    public GameObject FindClosestSmArea()
    {
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag("destin");
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject go in gos)
        {
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                closest = go;
                distance = curDistance;
            }
        }
        return closest;
    }
}
