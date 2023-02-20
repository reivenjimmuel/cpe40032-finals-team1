using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;

// This class provides animated indication of the player powerup
public class InfiniteRotate : MonoBehaviour
{
    public float rotateSpeed = 40;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up, rotateSpeed * Time.deltaTime);
    }
}
