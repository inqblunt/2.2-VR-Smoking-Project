using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SmokeMovementScript : MonoBehaviour
{
    public NavMeshAgent smoke;
    public Transform[] points;
    private int destPoint = 0;
    private bool toldOff = false;
    public GameObject target;
    bool smokeActive = false;
    public static bool inBox = false;
    bool isWorking;
    public AudioSource BoxSource;
    public AudioClip PopClip;

    List<Transform> pointList = new List<Transform>();

    void Start()
    {
        BoxSource.clip = PopClip;
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

        //smoke.autoRepath = true;
        //var rad = transform.GetComponent<NavMeshAgent>();
        //rad.radius = 0.00001f;
        GotoNextPoint();
    }

    void Update()
    {
        if (TimerScript.secondIsStarted == true)
        {
            GameObject closestWarden = FindClosestWarden();
            float dist = Vector3.Distance(closestWarden.transform.position, transform.position);
            // Choose the next destination point when the agent gets
            // close to the current one.
            if (!smoke.pathPending && smoke.remainingDistance < 0.5f && isWorking == false)
            {
                StartCoroutine(StudentWork());
                isWorking = true;
                Debug.Log("work");
            }
            if (dist > 2f && toldOff == false && isWorking == false)
            {
                toldOff = true;
                target = FindClosestSmArea();
                GotoSmPoint();
            }
            if (inBox == true && isWorking == false)
            {
                BoxSource.Play();
                StartCoroutine(StudentSmoke());
                isWorking = true;
            }
        }
        if (gameObject.GetComponent<OVRGrabbable>().isGrabbed == true)
        {
            transform.GetComponent<NavMeshAgent>().enabled = false;
        }
        else
        {
            transform.GetComponent<NavMeshAgent>().enabled = true;
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

    public GameObject FindClosestWarden()
    {
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag("warden");
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
        smoke.destination = points[destPoint].position;
        Debug.Log(smoke.destination);
        // Choose the next point in the array as the destination,
        // cycling to the start if necessary.
        destPoint = (destPoint + 1) % points.Length;

        int willSmoke = Random.Range(0, 99);

        if (willSmoke < 40)
        {
            TimerScript.isSmoking++;
            smokeActive = true;
            transform.Find("cigarette").GetComponent<MeshRenderer>().enabled = true;
            transform.Find("Point Light").GetComponent<Light>().enabled = true;
        }
        Debug.Log("Gotofire");
    }

    void GotoSmPoint()
    {
        if (points.Length == 0)
            return;

        target = FindClosestSmArea();
        smoke.destination = target.transform.position;
        //destPoint = (destPoint + 1) % points.Length;
    }

    IEnumerator StudentWork()
    {
        if (smokeActive == true)
        {
            smokeActive = false;
            TimerScript.isSmoking--;
            transform.Find("cigarette").GetComponent<MeshRenderer>().enabled = false;
            transform.Find("Point Light").GetComponent<Light>().enabled = false;
        }
        transform.GetComponent<NavMeshAgent>().isStopped = true;
        //transform.GetComponent<Rigidbody>().velocity = Vector3.zero;
        transform.GetComponent<MeshRenderer>().enabled = false;
        transform.GetComponent<SphereCollider>().enabled = false;
        yield return new WaitForSeconds(Random.Range(3,5));
        transform.GetComponent<MeshRenderer>().enabled = true;
        transform.GetComponent<SphereCollider>().enabled = true;
        transform.GetComponent<NavMeshAgent>().isStopped = false;
        isWorking = false;
        GotoNextPoint();
    }

    IEnumerator StudentSmoke()
    {
        transform.GetComponent<NavMeshAgent>().isStopped = true;
        //transform.GetComponent<Rigidbody>().velocity = Vector3.zero;
        yield return new WaitForSeconds(10);
        if (toldOff == true)
        {
            transform.Find("cigarette").GetComponent<MeshRenderer>().enabled = false;
            transform.Find("Point Light").GetComponent<Light>().enabled = false;
        }
        transform.GetComponent<NavMeshAgent>().isStopped = false;
        toldOff = false;
        isWorking = false;
        GotoNextPoint();
    }
}