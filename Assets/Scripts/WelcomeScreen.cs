using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WelcomeScreen : MonoBehaviour {
    public GameObject storyGO, helpGO, rescuePrinceGO, princeGO, princessGO, instructionsGO, instructionsImageGO;
    // Use this for initialization
    /// <summary>
    /// set most game objects off - to be turned on after the "story" is played
    /// </summary>
    void Start () {
        storyGO.SetActive(true);
        helpGO.SetActive(false);
        rescuePrinceGO.SetActive(false);
        princeGO.SetActive(false);
        princessGO.SetActive(false);
        instructionsGO.SetActive(false);
        instructionsImageGO.SetActive(false);
        StartCoroutine(DeactivateStory()); 
    }

    /// <summary>
    /// turn off the story and turns on the other game objects
    /// </summary>
    /// <returns>wait for 30 seconds</returns>
    private IEnumerator DeactivateStory()
    {
        yield return new WaitForSeconds(30);

        //Game object will turn off
        storyGO.SetActive(false);

        helpGO.SetActive(true);
        rescuePrinceGO.SetActive(true);
        princeGO.SetActive(true);
        princessGO.SetActive(true);
        instructionsGO.SetActive(true);
        instructionsImageGO.SetActive(true);
    }

}
