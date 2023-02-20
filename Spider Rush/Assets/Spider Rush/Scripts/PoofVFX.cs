using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoofVFX : MonoBehaviour
{
    public ParticleSystem poofParticle, deathParticle;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            deathParticle.Play();
        }

        else if (other.gameObject.CompareTag("Enemies"))
        {
            poofParticle.Play();
            Destroy(other.gameObject);
        }
    }
}
