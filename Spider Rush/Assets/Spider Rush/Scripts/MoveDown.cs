using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveDown : MonoBehaviour
{
    public float speed;
    private float zDestroy = -60.0f;
    private Rigidbody objectRb;
    private RepeatBackground repeatBackgroundScript;

    void Start()
    {
        objectRb = GetComponent<Rigidbody>();
        repeatBackgroundScript = GameObject.Find("Table").GetComponent<RepeatBackground>();
    }

    void Update()
    {
        MoveObject();
    }

    void MoveObject()
    {
        speed = repeatBackgroundScript.speed;
        objectRb.velocity = Vector3.forward * -speed;

        if (transform.position.z < zDestroy)
        {
            Destroy(gameObject);
        }
    }
}
