using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// Winning scene to display the high score and the score you recieved.
/// </summary>
public class WinnerScene : MonoBehaviour {
    public Text yourScoreText;
    public Text highScoreText;

    private int highScore;
    // Use this for initialization
    void Start () {
        string scoreMessage = "YOUR SCORE = " + PlayerPrefs.GetInt("score");
        yourScoreText.text = scoreMessage;
        SetHighScore();
    }

    private void SetHighScore()
    {
        //int highScore = PlayerPrefs.GetInt("highScore");
        if (PlayerPrefs.GetInt("highScore") <= PlayerPrefs.GetInt("score"))
        {
            PlayerPrefs.SetInt("highScore", PlayerPrefs.GetInt("score"));
        }
        string highScoreMessage = "HIGHEST SCORE = " + PlayerPrefs.GetInt("highScore");
        highScoreText.text = highScoreMessage;
    }


}
