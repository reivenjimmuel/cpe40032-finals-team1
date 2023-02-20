using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadMusic : MonoBehaviour
{
    private AudioListener audioListener;
	private AudioSource music;
	
	private int random;

    // Start is called before the first frame update
    void Start()
    {
        audioListener = GetComponent<AudioListener>();
		music = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    // Play music on game start
	public void PlayMusic()
    {
        music.Play();
    }
	
	// Stop music when game over
	public void StopMusic()
	{
		music.Stop();
	}
}
