using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SmokeMovementScript : MonoBehaviour
{
    public NavMeshAgent smoke;
    void Start()
    {

    }

    void Update()
    {
        if (TimerScript.secondIsStarted == true)
        {
            GameObject target = FindClosestSmArea();
            Transform targetTransform = target.GetComponent<Transform>();
            smoke.SetDestination(targetTransform.position);
        }
    }

    public GameObject FindClosestSmArea()
    {
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag("smarea");
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