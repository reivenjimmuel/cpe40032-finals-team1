using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayWebVFX : MonoBehaviour
{
    public float speed = 40.0f;
    private float zDestroy = 130.0f;

    public ParticleSystem bumpParticle, killParticle;
    public AudioClip enemySound;

    private AudioSource playerAudio;
    private Rigidbody objectRb;
    private PlayerController playerControllerScript;

    // Start is called before the first frame update
    void Start()
    {
        objectRb = GetComponent<Rigidbody>();
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
        playerAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        MoveWeb();
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Enemies"))
        {
            playerAudio.PlayOneShot(enemySound, 1.0f);
            killParticle.Play();
            Destroy(gameObject, 0.5f);
        }

        else if (other.gameObject.CompareTag("Obstacles"))
        {
            bumpParticle.Play();
            Destroy(gameObject, 0.5f);
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

