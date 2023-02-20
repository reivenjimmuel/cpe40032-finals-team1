using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundRepeat : MonoBehaviour
{
    public GameObject groundPrefab;
    private float speed = 50f;
    private Vector3 startPos;
    private float repeatWidth;

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
        repeatWidth = startPos.z/2;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (transform.position.z < startPos.z - repeatWidth)
        {
            Instantiate(groundPrefab, new Vector3(0, 0, repeatWidth), transform.rotation);
            Destroy(gameObject, 10);
        }
        // move the bacground backwards
        transform.Translate(Vector3.back * speed * Time.deltaTime);
    }
}
