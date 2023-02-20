using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class PlayerController : MonoBehaviour
{
    public GameObject gameManager;
    public GameObject projectilePrefab;
    public GameObject projectilePrefabVFX;
    public GameObject lifeIndicator;
    public bool gameOver;

    private Rigidbody playerRb;
    private Animator playerAnim;

    public int lives = 3;
    public int points = 0;
    // Store the player's current level
    public int currentLevel = 1;

    public float levelUpThreshold = 50.0f; // Threshold for leveling up
    public float zBound = 80f;
    public float speed = 15.0f; // Initial speed of the player
    // Store the player's current movement input
    private float moveX = 0f;
    private float moveZ = 0f;
	
	
	// FROM ASSETS OF SFX AND PARTICLE FX
	public ParticleSystem deathParticle, livesParticle, coinParticle, heartParticle, bumpParticle, starParticle;
	public AudioClip enemySound, deathSound, pointSound, lifeSound, webSound;
    private AudioSource playerAudio;

    private RepeatBackground repeatBackgroundScript, repeatWall;
    private InfiniteRotate rotateBroom1, rotateBroom2;
    private SpawnManager spawnManagerScript;

    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
		playerAudio = GetComponent<AudioSource>();
        playerAnim = GetComponentInChildren<Animator>();

        repeatBackgroundScript = GameObject.Find("Table").GetComponent<RepeatBackground>();
        repeatWall = GameObject.Find("Wall").GetComponent<RepeatBackground>();
        rotateBroom1 = GameObject.Find("Broom1").GetComponent<InfiniteRotate>();
        rotateBroom2 = GameObject.Find("Broom2").GetComponent<InfiniteRotate>();
        spawnManagerScript = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();

        Debug.Log("Lives: 3     Points: 0");
    }

    void Update()
    {
        GetInput();
        MovePlayer();
        ConstraintPlayerPosition(); //boundaries for player to move
		LifeIndicator(); // indicator
        SpawnWeb(); // spawns the projectile

        // Check if the player has reached the level up threshold
        if (points >= levelUpThreshold)
        {
            LevelUp();
        }

        gameManager.GetComponent<GameManager>().points = points;
    }
	
	// when player gets life, show an indicator
	void LifeIndicator()
    {
        lifeIndicator.transform.position = transform.position + new Vector3(0, -0.5f, 0);
    }

    void GetInput()
    {
        moveX = Input.GetAxis("Horizontal");
        moveZ = Input.GetAxis("Vertical");
    }

    void MovePlayer()
    {
        if (!gameOver)
        {
            playerRb.velocity = new Vector3(moveX, 0, moveZ) * speed;
        }
    }

    void ConstraintPlayerPosition()
    {
        if (transform.position.z > zBound)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, zBound);
        }
    }

    void SpawnWeb()
    {
		Vector3 offset1 = transform.position + new Vector3(0, 2, 2);
        Vector3 offset2 = transform.position + new Vector3(0, 2, 2.2f);
        if (Input.GetKeyDown(KeyCode.Space) && !gameOver && !PauseMenu.isGamePaused)
        {
            Instantiate(projectilePrefab, offset1, projectilePrefab.transform.rotation);
            Instantiate(projectilePrefabVFX, offset2, projectilePrefabVFX.transform.rotation);
            playerAudio.PlayOneShot(webSound, 1.0f);
        }
    }

    void LevelUp()
    {
        // Increase the player's level by 1
        currentLevel++;

        // Increase the player's speed by 5
        speed += 5;

        // Increase the level up threshold by 50
        levelUpThreshold *= 2;

        Debug.Log("Congratulations! You have reached level " + currentLevel.ToString() + "!");
        Debug.Log("Your speed has increased to " + speed.ToString() + " and your new level up threshold is " + levelUpThreshold.ToString() + " points.");

        repeatBackgroundScript.speed += 5;
        repeatWall.speed += 5;
        spawnManagerScript.spawnInterval -= 0.3f;
        spawnManagerScript.StartSpawn();
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Enemies"))
        {
            playerAudio.PlayOneShot(enemySound, 1.0f);
            --lives;
            Debug.Log("Lives: " + (lives).ToString() + "     Points: " + points.ToString());
            gameManager.GetComponent<GameManager>().lives = lives;
            bumpParticle.Play();

            if (lives == 0)
            {
                gameOver = true;

                playerAnim.SetBool("Death_Anim", true);
                playerAudio.PlayOneShot(deathSound, 1.0f);

                //show restart btn and stop music
                gameManager.GetComponent<GameManager>().restartButton.SetActive(true);
                gameManager.GetComponent<GameManager>().GameOver();
                Debug.Log("Game Over. Your final score is " + points.ToString());
                deathParticle.Play();

                DestroyAllEnemies();
                DestroyAllPoints();
                DestroyAllLife();
                repeatBackgroundScript.speed = 0;
                repeatWall.speed = 0;
                rotateBroom1.rotateSpeed = 0;
                rotateBroom2.rotateSpeed = 0;
            }
            Destroy(other.gameObject);
        }

        else if (other.gameObject.CompareTag("Life"))
        {
            ++lives;
            Debug.Log("Lives: " + lives.ToString() + "     Points: " + points.ToString());
            gameManager.GetComponent<GameManager>().lives = lives;
            heartParticle.Play();
            Destroy(other.gameObject);

            // when bonus lives are obtained, play sound and show life indicator
            playerAudio.PlayOneShot(lifeSound, 1.0f);
            lifeIndicator.gameObject.SetActive(true);
            StartCoroutine(LivesCountdownRoutine());
        }

        else if (other.gameObject.CompareTag("Points"))
        {
            points += 10;
            Debug.Log("Lives: " + lives.ToString() + "     Points: " + (points).ToString());
            gameManager.GetComponent<GameManager>().points = points;
            Destroy(other.gameObject);
            coinParticle.Play();
            starParticle.Play();

            // play sound when player obtains point items
            playerAudio.PlayOneShot(pointSound, 1.0f);
        }

        else if (other.gameObject.CompareTag("Broom"))
        {
            playerRb.AddForce(Vector3.forward * -900, ForceMode.Impulse);

            //StartCoroutine(DeathCountdownRoutine());
            playerAnim.SetBool("Death_Anim", true);
            playerAudio.PlayOneShot(deathSound, 1.0f);

            //show restart btn and stop music
            gameOver = true;
            gameManager.GetComponent<GameManager>().restartButton.SetActive(true);
            gameManager.GetComponent<GameManager>().GameOver();
            Debug.Log("Game Over. Your final score is " + points.ToString());
            deathParticle.Play();

            DestroyAllEnemies();
            DestroyAllPoints();
            DestroyAllLife();
            repeatBackgroundScript.speed = 0;
            repeatWall.speed = 0;
            rotateBroom1.rotateSpeed = 0;
            rotateBroom2.rotateSpeed = 0;
        }
    }

    void DestroyAllEnemies()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemies");
        foreach (GameObject enemy in enemies)
        {
            GameObject.Destroy(enemy);
        }
    }

    void DestroyAllPoints()
    {
        GameObject[] points = GameObject.FindGameObjectsWithTag("Points");
        foreach (GameObject point in points)
        {
            GameObject.Destroy(point);
        }
    }

    void DestroyAllLife()
    {
        GameObject[] lives = GameObject.FindGameObjectsWithTag("Life");
        foreach (GameObject life in lives)
        {
            GameObject.Destroy(life);
        }
    }

    // Duration of the lives particle.
    IEnumerator LivesCountdownRoutine()
	{
		livesParticle.gameObject.SetActive(true);
		yield return new WaitForSeconds(3);
		livesParticle.gameObject.SetActive(false);
	}
}