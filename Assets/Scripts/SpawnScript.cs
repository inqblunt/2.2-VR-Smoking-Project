using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SpawnScript : MonoBehaviour {

    public float spawnTime = 10f;
    public int spawnCount = 0;
    public GameObject smoke;
    public GameObject nosmoke;
    public Transform[] spawnPoints;         // An array of the spawn points this enemy can spawn from.
    public GameObject[] spawners;
    public NavMeshSurface surface;

    public static int nosmokesDestroyed = 0;

    // Use this for initialization
    void Start () {
    }
	
	// Update is called once per frame
	void Update () {
	}

    public void ButtonSwitch ()
    {
        spawners = GameObject.FindGameObjectsWithTag("spawn");
        for (int i = 0; i < spawners.Length; i++)
        {
            spawnPoints[i] = spawners[i].transform;
            //Debug.Log(spawnPoints[i]);
        }

        GameObject.Find("Area 1").GetComponent<Collider>().enabled = false;
        GameObject.Find("Area 2").GetComponent<Collider>().enabled = false;
        GameObject.Find("Warden").GetComponent<Collider>().enabled = false;
        GameObject.Find("Warden1").GetComponent<Collider>().enabled = false;
        GameObject.Find("Warden").GetComponent<Rigidbody>().useGravity = false;
        GameObject.Find("Warden1").GetComponent<Rigidbody>().useGravity = false;
        GameObject.Find("Warden").GetComponent<OVRGrabbable>().enabled = false;
        GameObject.Find("Warden1").GetComponent<OVRGrabbable>().enabled = false;
        GameObject.Find("Area 1").GetComponent<Rigidbody>().useGravity = false;
        GameObject.Find("Area 2").GetComponent<Rigidbody>().useGravity = false;
        GameObject.Find("Area 1").GetComponent<OVRGrabbable>().enabled = false;
        GameObject.Find("Area 2").GetComponent<OVRGrabbable>().enabled = false;

        surface.BuildNavMesh();
        InvokeRepeating("Spawn", spawnTime, spawnTime);
    }

    void Spawn ()
    {
        if (spawnCount >= 16)
        {
            return;
        }
        int spawnPointIndex = Random.Range(0, spawnPoints.Length);

        //Debug.Log(spawnPoints[spawnPointIndex].position);
       // newNosmoke.transform.SetParent(GameObject.Find("CharacterManager").transform, false);
       if (spawnCount % 4 == 0) { 
        var newSmoke = Instantiate(smoke, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
       }
       else
        {
            var newSmoke = Instantiate(smoke, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
        }
        // newSmoke.transform.SetParent(GameObject.Find("CharacterManager").transform, false);

        spawnCount++;
    }
}
