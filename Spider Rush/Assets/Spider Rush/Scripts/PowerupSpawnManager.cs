using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupSpawnManager : MonoBehaviour
{
    public GameObject[] objectPrefabs;
    public float spawnDelay = 3;
    public float spawnInterval = 15f;
    public float spawnRangeX = 40;
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
        Vector3 spawnLocation = new Vector3(Random.Range(-spawnRangeX, spawnRangeX), 5, spawnPosZ);
        int index = Random.Range(0, objectPrefabs.Length);

        if (!playerControllerScript.gameOver)
        {
            Instantiate(objectPrefabs[index], spawnLocation, objectPrefabs[index].transform.rotation);
        }
    }
}
