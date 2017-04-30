using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Player class for second level
/// </summary>
public class PlayerCastle : MonoBehaviour {
    
    public GameObject shieldGO, keysMsg, loveGO;
    public AudioClip keyPickUp;
    public AudioClip gemPickUp;
    public AudioClip dieSound;
    public AudioClip foundPrinceSound;

    private float delayBetweenDeaths = 5f;
    private float nextTimeAllowedToDie = 0;

    private int lives;
    private int score;
    private int keys = 0;
    private PlayerDisplay playerDisplay;
    

    // Use this for initialization
    void Start ()
    {
        score = PlayerPrefs.GetInt("score");
        lives = PlayerPrefs.GetInt("lives");
        playerDisplay = GetComponent<PlayerDisplay>();
        playerDisplay.UpdateScoreText(score);
        playerDisplay.UpdateKeysText(keys);
        playerDisplay.UpdateLivesImage(lives);
        keysMsg.SetActive(false);
        loveGO.SetActive(false);
    }

    // Update is called once per frame
    void Update ()
    {
       CheckScore();
       DisplayShieldIfCannotDie();
    }

    /// <summary>
    /// Check to see if score is below 0, if it is - then lose a life
    /// </summary>
    private void CheckScore()
    {
        score = PlayerPrefs.GetInt("score");
        if (score < 0)
        {
            PlayerPrefs.SetInt("score", 0);
            score = PlayerPrefs.GetInt("score");
            playerDisplay.UpdateScoreText(score);
            LoseLife();
        }
    }

    //-----------------------------
    /// <summary>
    /// Lose a life
    /// </summary>
    public void LoseLife()
    {
        if (Time.time > nextTimeAllowedToDie)
        {
            lives--;
            PlayerPrefs.SetInt("lives", lives);

            MoveToStartPosition();
            GetComponent<AudioSource>().PlayOneShot(dieSound);
            // update our next time allowed to die for a future time ...
            nextTimeAllowedToDie = Time.time + delayBetweenDeaths;
        }
        if (lives < 0)
        {
            SceneManager.LoadScene("scene1_GameOver");
            PlayerPrefs.SetInt("lives", 3);
            PlayerPrefs.SetInt("score", 0);  
        }
        playerDisplay.UpdateLivesImage(lives);
    }

    /// <summary>
    /// If you lose a life, move player back to the start position of game
    /// </summary>
    private void MoveToStartPosition()
    {
        Vector3 startPosition = new Vector3(-13.2f, -9.5f, 0);
        transform.position = startPosition;

        // remove all horizontal/vertical movement when respawned
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
    }

    //---------------------------------
    public void DisplayShieldIfCannotDie()
    {
        bool cannotDie = (Time.time <= nextTimeAllowedToDie);
        // show shield if cannot die
        // else hide it
        shieldGO.SetActive(cannotDie);
    }

    /*void OnTriggerStay2D(Collider2D collide)
    {

        if (collide.gameObject.tag == "platformMove")
        {
            transform.parent = collide.transform;

        }
    }*/

    void OnTriggerExit2D(Collider2D collide)
    {
        if (collide.CompareTag("platformMove"))
        {
            transform.parent = null;
        }
    }

    void OnTriggerEnter2D(Collider2D hit)
    {
        //if hero lands on a moving platform, she will move with it
        if (hit.CompareTag("platformMove"))
        {
            transform.parent = hit.transform;
        }else if (hit.CompareTag("NoParent"))
        {
            transform.parent = null;
        }

        else if (hit.CompareTag("GemBlue"))
        {
            score = PlayerPrefs.GetInt("score");
            score++;
            PlayerPrefs.SetInt("score", score);
            
            playerDisplay.UpdateScoreText(score);
            Destroy(hit.gameObject);
            GetComponent<AudioSource>().PlayOneShot(gemPickUp);
        }
    
        else if (hit.CompareTag("Key"))
        {
            keys++;
            playerDisplay.UpdateKeysText(keys);
            Destroy(hit.gameObject);
            GetComponent<AudioSource>().PlayOneShot(keyPickUp);
        }
        else if (hit.CompareTag("Torch"))
        {
            Destroy(hit.gameObject);
            Destroy(GameObject.Find("black-foreground"));
        }
        else if (hit.CompareTag("Door"))
        {
            CheckNumKeys();   
        }
        if (hit.CompareTag("Spikes"))
        {
            LoseLife();
        }
        if (hit.CompareTag("Prince"))
        {
            loveGO.SetActive(true);
            GetComponent<AudioSource>().PlayOneShot(foundPrinceSound);
            StartCoroutine(LoadGameWon());  
        }
    }
    /// <summary>
    /// Wait 2 seconds and load the game won scene
    /// </summary>
    /// <returns></returns>
    private IEnumerator LoadGameWon()
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("scene3_GameWon");
    }

    /// <summary>
    /// Check the correct number of keys are present to open the door
    /// </summary>
    private void CheckNumKeys()
    {
        if (keys == 3)
        {
            Destroy(GameObject.Find("door"));
            Destroy(GameObject.Find("castleWall"));
        }
        else
        {
            StartCoroutine(ActivateKeysMsg());
        }
    }

    private IEnumerator ActivateKeysMsg()
    {
        //Turn My game object that is set to false(off) to True(on).
        keysMsg.SetActive(true);

        //Turn the Game Oject back off after 7 sec.
        yield return new WaitForSeconds(7);

        //Game object will turn off
        keysMsg.SetActive(false);
    }
}
