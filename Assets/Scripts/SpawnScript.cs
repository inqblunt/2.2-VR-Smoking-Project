using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SpawnScript : MonoBehaviour {

    public float spawnTime = 3f;
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

        GameObject.Find("SmallCube").GetComponent<Collider>().enabled = false;
        GameObject.Find("SmallCube").GetComponent<Rigidbody>().useGravity = false;
        GameObject.Find("SmallCube2").GetComponent<Collider>().enabled = false;
        GameObject.Find("BigCube").GetComponent<Collider>().enabled = false;
        GameObject.Find("BigCube2").GetComponent<Collider>().enabled = false;
        GameObject.Find("Area 1").GetComponent<Collider>().enabled = false;
        GameObject.Find("Area 2").GetComponent<Collider>().enabled = false;
        GameObject.Find("SmallCube2").GetComponent<Rigidbody>().useGravity = false;
        GameObject.Find("BigCube").GetComponent<Rigidbody>().useGravity = false;
        GameObject.Find("BigCube2").GetComponent<Rigidbody>().useGravity = false;
        GameObject.Find("Area 1").GetComponent<Rigidbody>().useGravity = false;
        GameObject.Find("Area 2").GetComponent<Rigidbody>().useGravity = false;

        surface.BuildNavMesh();
        InvokeRepeating("Spawn", spawnTime, spawnTime);
    }

    void Spawn ()
    {
        if (spawnCount >= 8)
        {
            return;
        }
        int spawnPointIndex = Random.Range(0, spawnPoints.Length);

        Debug.Log(spawnPoints[spawnPointIndex].position);
        var newNosmoke = Instantiate(nosmoke, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
        newNosmoke.transform.SetParent(GameObject.Find("CharacterManager").transform, false);
        var newSmoke = Instantiate(smoke, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
        newSmoke.transform.SetParent(GameObject.Find("CharacterManager").transform, false);

        spawnCount++;
    }
}
