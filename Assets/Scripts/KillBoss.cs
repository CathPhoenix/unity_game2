using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Class for interactions with the "boss"/big spider
/// </summary>
public class KillBoss : MonoBehaviour {
    public GameObject bossGO, plus15GO, minus10GO;
    public AudioClip killBossSound;
    private PlayerCastle playerCastle;
    private PlayerDisplay playerDisplay;
    private int score;
    private int countBossHits = 0;
    public GameObject finalKey;
    // Use this for initialization
    void Start()
    {
        score = PlayerPrefs.GetInt("score");
        playerCastle = GetComponent<PlayerCastle>();
        playerDisplay = GetComponent<PlayerDisplay>();
        finalKey.SetActive(false);
        plus15GO.SetActive(false);
        minus10GO.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D hit)
    {
        //if you get hit by the big spider, your points will be reduced.
        if (hit.CompareTag("SpiderBoss"))
        {
            
            StartCoroutine(ActivateMinus10());
            score = PlayerPrefs.GetInt("score");
            score -= 10;
            PlayerPrefs.SetInt("score", score);
            
            playerDisplay.UpdateScoreText(score);
            //if your points are reduced below 0, you lose a life and points are reset to 0 
            if (PlayerPrefs.GetInt("score") < 0)
            {
                PlayerPrefs.SetInt("score", 0);
                //score = PlayerPrefs.GetInt("score");
                score = PlayerPrefs.GetInt("score");
                playerDisplay.UpdateScoreText(score);
                playerCastle.LoseLife();
            }
        }
        //if you jump on top of the spider you get points added to your score and the number of hits needed to kill the big spider reduces
        else if (hit.CompareTag("KillBoss"))
        {
            //score += 15;
            StartCoroutine(ActivatePlus15());
            score = PlayerPrefs.GetInt("score");
            score += 15;
            PlayerPrefs.SetInt("score", score);
            
            playerDisplay.UpdateScoreText(score);
            countBossHits += 1;
            
            //if you have hit the spider more than 5 times, he is destroyed and the key is set to visible
            if (countBossHits > 5)
            {
                GetComponent<AudioSource>().PlayOneShot(killBossSound);
                Destroy(bossGO);
                finalKey.SetActive(true);
            }
        }
    }
    /// <summary>
    /// to activate the "-10" to be shown for one second when the spider hits you
    /// </summary>
    /// <returns></returns>
    private IEnumerator ActivateMinus10()
    {
        minus10GO.SetActive(true);

        //Turn the Game Oject back off after 1 sec.
        yield return new WaitForSeconds(1);

        //Game object will turn off
        minus10GO.SetActive(false);
    }
    /// <summary>
    /// to activate the "+15" to be shown for one second when you jump on the big spider
    /// </summary>
    /// <returns></returns>
    private IEnumerator ActivatePlus15()
    {
        plus15GO.SetActive(true);

        //Turn the Game Oject back off after 1 sec.
        yield return new WaitForSeconds(1);

        //Game object will turn off
        plus15GO.SetActive(false);
    }
}
