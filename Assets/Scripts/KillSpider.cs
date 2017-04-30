using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class to deal with killing the small spiders
/// </summary>
public class KillSpider : MonoBehaviour
{

    public GameObject spiderGO1, spiderGO2;
    private PlayerCastle playerCastle;
    private PlayerDisplay playerDisplay;
    private int score;
    public AudioClip killSpiderSound;
    // Use this for initialization
    void Start()
    {
        score = PlayerPrefs.GetInt("score");
        playerCastle = GetComponent<PlayerCastle>();
        playerDisplay = GetComponent<PlayerDisplay>();
    }

    void OnTriggerEnter2D(Collider2D hit)
    {
        //if you get hit by a spider, you lose a life.
        if (hit.CompareTag("spider"))
        {
            playerCastle.LoseLife();
        }
        if (hit.CompareTag("KillSpider"))
        {
            KillTheSpider(spiderGO1);
           
        }
        if (hit.CompareTag("KillSpider2"))
        {
            KillTheSpider(spiderGO2);
          
        }
    }
    /// <summary>
    /// When the spider is jumped on, add points to your score and destroy the spider
    /// </summary>
    /// <param name="spider">the spider GO that was jumped on</param>
    void KillTheSpider(GameObject spider)
    {
        score = PlayerPrefs.GetInt("score");
        score += 10;
        PlayerPrefs.SetInt("score", score);
        
        playerDisplay.UpdateScoreText(score);
        GetComponent<AudioSource>().PlayOneShot(killSpiderSound);
        Destroy(spider);
    }
}
