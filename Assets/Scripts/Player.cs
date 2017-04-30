using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System;
/// <summary>
/// For player actions in level 1
/// </summary>
public class Player : MonoBehaviour {
	public GameObject shieldGO;
    public GameObject gemPink;
    public GameObject keysMsg;
    public GameObject door;
   
    public AudioClip keyPickUp;
    public AudioClip gemPickUp;
	public AudioClip dieSound;
    public AudioClip pinkGemPickUp;

    PlayerControlLadder playerControlLadder;
    JumpCountdown jumpCountdown;

	private float delayBetweenDeaths = 5f;
	private float nextTimeAllowedToDie = 0;

    private float timeToJumpBig = 10f;
    private float timeToGoBackToJumpNormal = 0;

    private PlayerDisplay playerDisplay;

	private int lives = 3;
	private int score;
    private int keys = 0;
    private int jumpSecondsLeft = 0;
    private int respawnNum = 1;
	//private float deathY = -15;
    

    //-----------------------------
    void Start()
	{
        score = 0;
        PlayerPrefs.SetInt("score", score);
        PlayerPrefs.SetInt("lives", lives);
        playerControlLadder = GetComponent<PlayerControlLadder>();     
        jumpCountdown = GetComponent<JumpCountdown>();

		playerDisplay = GetComponent<PlayerDisplay>();
		playerDisplay.UpdateScoreText(score);
        playerDisplay.UpdateKeysText(keys);
        playerDisplay.UpdateLivesImage(lives);
        keysMsg.SetActive(false);
	}
	
	//-----------------------------
	void Update()
	{
		DisplayShieldIfCannotDie();
        NormalJumpIfTimeUp();
        DisplayJumpCountDown(); 
    }

    //---------------------------------
    /// <summary>
    /// Display the jump countdown image relative to how many seconds are remaining
    /// </summary>
    private void DisplayJumpCountDown()
    {    
        jumpSecondsLeft = jumpCountdown.GetSecondsRemaining();
        playerDisplay.UpdateJumpImage(jumpSecondsLeft);
    }

    //---------------------------------
    /// <summary>
    /// If you are allowed to jump high, then turn on the jump high method
    /// </summary>
    private void NormalJumpIfTimeUp()
    {
        bool timeAllowedToJump = (Time.time >= timeToGoBackToJumpNormal);
        playerControlLadder.SetJumpForceNormal(timeAllowedToJump);   
    }
    //---------------------------------

    public void DisplayShieldIfCannotDie()
	{
		bool cannotDie = (Time.time <= nextTimeAllowedToDie);
		// show shield if cannot die
		// else hide it
		shieldGO.SetActive( cannotDie );
	}

	//-----------------------------
    /// <summary>
    /// Lose a life
    /// </summary>
	private void LoseLife()
	{
        //if you don't hav your invincibility shield, you can die..
		if(Time.time > nextTimeAllowedToDie){
			lives--;
            PlayerPrefs.SetInt("lives", lives);
            MoveToRespawnPosition();
            GetComponent<AudioSource>().PlayOneShot(dieSound);
			// update our next time allowed to die for a future time ...
			nextTimeAllowedToDie = Time.time + delayBetweenDeaths;
		}

        //if you have less than 0 lives, load the game over scene
		if(lives < 0)
		{
            SceneManager.LoadScene("scene1_GameOver");
            PlayerPrefs.SetInt("score", 0);
            PlayerPrefs.SetInt("lives", 3);  
		}
		//update the lives image, passing how many lives are left
		playerDisplay.UpdateLivesImage(lives);
    }

	//-----------------------------
    /// <summary>
    /// Depending on where you die, move the hero to the relative respawn position
    /// </summary>
	private void MoveToRespawnPosition()
	{
        Vector3 startPosition;
        //GameObject respawnGO = ChooseRandomObjectWithTag("Respawn");
        if (respawnNum == 1)
        {
            startPosition = GameObject.Find("respawn-point1").transform.position;
        }else if (respawnNum == 2)
        {
            startPosition = GameObject.Find("respawn-point2").transform.position;
        }
        else 
        {
            startPosition = GameObject.Find("respawn-point3").transform.position;
        }

        transform.position = startPosition;

        // remove all horizontal/vertical movement when respawned
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
    }

	//-----------------------------
	void OnTriggerEnter2D(Collider2D hit)
	{
        //add one to score if you collect a blue gem
        if (hit.CompareTag("GemBlue"))
		{
            PlayerPrefs.SetInt("score", ++score);
            score = PlayerPrefs.GetInt("score");
            playerDisplay.UpdateScoreText(score);
            Destroy(hit.gameObject);
            GetComponent<AudioSource>().PlayOneShot(gemPickUp);
        }
        //lose a life if you hit the spikes
        else if(hit.CompareTag("Spikes"))
		{
			LoseLife();
		}
        //if you get to the door, check how many keys have been collected
        else if (hit.CompareTag("Door"))
        {
            CheckNumKeys();
        }
        //if you pick up the special gem that make you jump high, turn on the jump high
        else if (hit.CompareTag("Jump"))
        {
            GetComponent<AudioSource>().PlayOneShot(pinkGemPickUp);
            playerControlLadder.SetJumpForceBig();
            
            timeToGoBackToJumpNormal = Time.time + timeToJumpBig;
            jumpCountdown.SetTimer(10);
            playerDisplay.UpdateJumpImage(10);

            //turn off the special gem
            gemPink.SetActive(false);  
        }
        //collect keys
        else if (hit.CompareTag("Key"))
        {
            keys++;
            playerDisplay.UpdateKeysText(keys);
            Destroy(hit.gameObject);
            GetComponent<AudioSource>().PlayOneShot(keyPickUp);
        }
        else if (hit.CompareTag("Respawn1"))
        {
            respawnNum = 1;
        }else if (hit.CompareTag("Respawn2"))
        {
            respawnNum = 2;
        }else if (hit.CompareTag("Respawn3"))
        {
            respawnNum = 3;
        }
    }
    /// <summary>
    /// check the number of keys collected.
    /// </summary>
    private void CheckNumKeys()
    {
        // if you have 3 keys you can proceed to level 2
        if (keys == 3)
        {
            door.SetActive(false);
            StartCoroutine(LoadNewScene()); //start a timer, and load next scene
        }
        else // if you don't have 3 keys, show a message for a number of seconds telling player to go get more keys
        {
            StartCoroutine(ActivateKeysMsg());
        }
    }

    /// <summary>
    /// Timer to display the prompt for the player for 7 seconds
    /// </summary>
    /// <returns>wait for 7 seconds</returns>
    private IEnumerator ActivateKeysMsg()
    {

        //Turn My game object that is set to false(off) to True(on).
        keysMsg.SetActive(true);

        //Turn the Game Oject back off after 7 sec.
        yield return new WaitForSeconds(7);

        //Game object will turn off
        keysMsg.SetActive(false);
    }

    /// <summary>
    /// Load the next level after one second
    /// </summary>
    /// <returns>wait for 1 second</returns>
    private IEnumerator LoadNewScene()    {

        //Wait 1 second
        yield return new WaitForSeconds(1);

        SceneManager.LoadScene("scene4_Level2Playing");
    }

}
