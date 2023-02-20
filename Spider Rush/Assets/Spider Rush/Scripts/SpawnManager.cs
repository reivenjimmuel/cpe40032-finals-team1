using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] objectPrefabs;
    public float spawnDelay = 1f;
    public float spawnInterval = 1.5f;
    public float spawnRangeX = 50;
    public float spawnPosZ = 130;

    private PlayerController playerControllerScript;

    void Start()
    {
        InvokeRepeating("SpawnObjects", spawnDelay, spawnInterval);
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    void Update()
    {

    }

    void SpawnObjects()
    {
        Vector3 spawnLocation = new Vector3(Random.Range(-spawnRangeX, spawnRangeX), 2.75f, spawnPosZ);
        int index = Random.Range(0, objectPrefabs.Length);

        if (!playerControllerScript.gameOver)
        {
            Instantiate(objectPrefabs[index], spawnLocation, objectPrefabs[index].transform.rotation);
        }
    }

    public void StartSpawn()
    {
        CancelInvoke("SpawnObjects");
        InvokeRepeating("SpawnObjects", spawnDelay, spawnInterval);
    }
}
