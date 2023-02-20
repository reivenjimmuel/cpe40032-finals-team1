using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnWeb : MonoBehaviour
{
    public GameObject gameManager;

    public float speed = 40.0f;
    private float zDestroy = 130.0f;

    private Rigidbody objectRb;
    private PlayerController playerControllerScript;
    

    void Start()
    {
        objectRb = GetComponent<Rigidbody>();
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    void Update()
    {
        MoveWeb();
    }
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Enemies"))
        {
            Destroy(gameObject);
            Destroy(other.gameObject);
            Debug.Log("Lives: " + playerControllerScript.lives.ToString() + "     Points: " + (playerControllerScript.points += 2).ToString());
        }

        else if (other.gameObject.CompareTag("Obstacles"))
        {
            Destroy(gameObject);
        }
    }

    void MoveWeb()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * speed);
        if (transform.position.z > zDestroy)
        {
            Destroy(gameObject);
        }
    }
}