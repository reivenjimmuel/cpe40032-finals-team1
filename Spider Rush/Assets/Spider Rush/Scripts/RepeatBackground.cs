using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatBackground : MonoBehaviour
{
    public Vector3 startPos;
    public float speed = 25;
    private Rigidbody objectRb;
    private float repeatWidth;

    void Start()
    {
        objectRb = GetComponent<Rigidbody>();
        startPos = transform.position;
        repeatWidth = GetComponent<BoxCollider>().size.z / 2;
    }

    void Update()
    {
        objectRb.velocity = Vector3.forward * -speed;

        if (transform.position.z < startPos.z - repeatWidth)
        {
            transform.position = startPos;
        }
    }
}
