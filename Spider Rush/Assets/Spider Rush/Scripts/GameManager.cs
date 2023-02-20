using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;


public class GameManager : MonoBehaviour
{
    // note: for cleaner code, reorganize the "gameover" variable
    // in the PlayerController.cs by putting it
    // here..., or let it be. because the player gameobject will not disappear.
    public bool gameOver = false;

    public LoadMusic loadMusic;

    private GameObject player, spawnManager, powerupSpawner, broom1, broom2;
    public GameObject hud, restartButton, playernameObject;
	
	public TextMeshProUGUI pointsText, livesText, highscoreText;

	public int points = 0, lives = 3;
	public int highscore;

    private string input, playername;


    private void Start()
    {
		
		// initializing points, lives as text and getting the prev highscore
		pointsText.text = "Points: " + points;
		livesText.text = "Health: " + lives.ToString();
		highscore = PlayerPrefs.GetInt("highscore", 0);
		playername =  PlayerPrefs.GetString("Player", "New Player");
		highscoreText.text = playername.ToString() + ": " + highscore.ToString();
		
        // getting the reference in the screen before
        player = GameObject.Find("Player");
        spawnManager = GameObject.Find("SpawnManager");
        powerupSpawner = GameObject.Find("PowerupSpawnManager");
        broom1 = GameObject.Find("Broom1");
        broom2 = GameObject.Find("Broom2");

        // deactivating the gameobjects to avoid errors
        player.SetActive(false);
        spawnManager.SetActive(false);
        powerupSpawner.gameObject.SetActive(false);
        broom1.SetActive(false);
        broom2.SetActive(false);

    }

    private void Update()
    {
		
        // for the scores to update and lives
		pointsText.text = "Points: " + points.ToString();
		livesText.text = "Health: " + lives.ToString();
		
		// setting a new highscore
		if (points > highscore)
		{
			PlayerPrefs.SetInt("highscore", points);
		}
		
		highscoreText.text = PlayerPrefs.GetString("Player", "New Player") + ": "+ highscore.ToString();
		
    }

    public void GameStart()
    {
        GameObjectActivate();
        loadMusic.PlayMusic();


    }

    public void GameOver()
    {
        // the PlayerController.cs is enough to stop the game,
        // but for code management use this if errors occur
        Debug.Log("Game Over...");
		loadMusic.StopMusic();
		hud.SetActive(false);
		
		// ask for username if highscore is beaten
		if(highscore < points)
		{
			playernameObject.gameObject.SetActive(true);
		}
		
    }

    public void RestartGame()
    {
        Debug.Log("Restarting the game...");
		// unfreezing the player movement and projectiles, and object spawning
		PauseMenu.isGamePaused = false;
		Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // redirecting to menu
    }

    public void QuitGame()
    {
		// IF YOU WANT TO CHECK IF THE PLAYER HIGHSCORE CAN BE RESET
		PlayerPrefs.DeleteAll();
        Debug.Log("quitting game...");
        Application.Quit();
        UnityEditor.EditorApplication.isPlaying = false;
    }
	
	// after confirmation of the user name input as new highscore, hide it again
	public void ReadStringInput(string s)
	{
		PlayerPrefs.SetString("Player", s);
		Debug.Log("Highscore by " + s.ToString() + ": " + highscore.ToString());
		playernameObject.gameObject.SetActive(false);
	}

    // handles the errors about missing references
    void GameObjectActivate()
    {
        // activating again the gameobjects, and the HUD - e.g.: score UI, life UI, level UI (unfinished)
        player.SetActive(true);
        spawnManager.SetActive(true);
        powerupSpawner.gameObject.SetActive(true);
        GameObject.Find("Main Menu").SetActive(false);
        broom1.SetActive(true);
        broom2.SetActive(true);

        player.GetComponent<PlayerController>().enabled = true;
        spawnManager.GetComponent<SpawnManager>().enabled = true;
        powerupSpawner.GetComponent<PowerupSpawnManager>().enabled = true;

        hud.SetActive(true);

        // note: add the UIs for the score, lives, Restart Button
    }
}
