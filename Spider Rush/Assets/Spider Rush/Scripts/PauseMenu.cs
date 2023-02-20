using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
	public static bool isGamePaused = false;
	public GameObject pauseMenuUI;
	
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			if (isGamePaused)
			{
				Resume();
			} else
			{
				Pause();
			}
		}
    }
	
	public void Resume()
	{
		pauseMenuUI.SetActive(false);
		Time.timeScale = 1f;
		isGamePaused = false;
	}
	
	public void Pause()
	{
		pauseMenuUI.SetActive(true);
		Time.timeScale = 0f;
		isGamePaused = true;
	}
}
