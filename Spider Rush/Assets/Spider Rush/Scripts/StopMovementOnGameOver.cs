using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopMovementOnGameOver : MonoBehaviour
{
    private Rigidbody objectRb;
    private PlayerController playerControllerScript;

    void Start()
    {
        objectRb = GetComponent<Rigidbody>();
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    void Update()
    {
        if (playerControllerScript.gameOver)
        {
            objectRb.velocity = Vector3.zero;
        }
    }
}
