using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NosmokeMovementScript : MonoBehaviour {

    public NavMeshAgent nosmoke;
    public Transform[] points;
    private int destPoint = 0;
    float dist;
    bool isWorking = false;

    List<Transform> pointList = new List<Transform>();

    // Use this for initialization
    void Start () {

        points[0] = GameObject.Find("GameObject (3)").transform;
        points[1] = GameObject.Find("GameObject (2)").transform;
        points[2] = GameObject.Find("GameObject (1)").transform;
        points[3] = GameObject.Find("GameObject").transform;

        pointList.Add(points[0]);
        pointList.Add(points[1]);
        pointList.Add(points[2]);
        pointList.Add(points[3]);

        for (int i = 0; i < pointList.Count; i++)
        {
            var temp = pointList[i];
            int randomIndex = Random.Range(i, pointList.Count);
            pointList[i] = pointList[randomIndex];
            pointList[randomIndex] = temp;
        }

        points[0] = pointList[0];
        points[1] = pointList[1];
        points[2] = pointList[2];
        points[3] = pointList[3];

        //nosmoke.autoBraking = false;
        nosmoke.autoRepath = true;
        //var rad = transform.GetComponent<NavMeshAgent>();
        //rad.radius = 0.00001f;
        GotoNextPoint();
    }
	
	// Update is called once per frame
     void Update () {
		if (TimerScript.secondIsStarted == true)
        {
            if (!nosmoke.pathPending && nosmoke.remainingDistance < 0.5f && isWorking == false)
            {
                StartCoroutine(StudentWork());
                isWorking = true;
            }
        }
        if (gameObject.GetComponent<OVRGrabbable>().isGrabbed == true)
        {
            transform.GetComponent<NavMeshAgent>().isStopped = true;
        }
        else
        {
            transform.GetComponent<NavMeshAgent>().isStopped = false;
        }
	}

    public GameObject FindClosestSmoke()
    {
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag("smoke");
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

    void GotoNextPoint()
    {
        // Returns if no points have been set up
        if (points.Length == 0)
            return;

        // Set the agent to go to the currently selected destination.
        nosmoke.destination = points[destPoint].position;

        // Choose the next point in the array as the destination,
        // cycling to the start if necessary.
        destPoint = (destPoint + 1) % points.Length;
    }

    IEnumerator StudentWork()
    {
        transform.GetComponent<NavMeshAgent>().isStopped = true;
        transform.GetComponent<Rigidbody>().velocity = Vector3.zero;
        transform.GetComponent<MeshRenderer>().enabled = false;
        transform.GetComponent<SphereCollider>().enabled = false;
        yield return new WaitForSeconds(Random.Range(3,10));
        transform.GetComponent<MeshRenderer>().enabled = true;
        transform.GetComponent<SphereCollider>().enabled = true;
        transform.GetComponent<NavMeshAgent>().isStopped = false;
        isWorking = false;
        GotoNextPoint();
    }
}
