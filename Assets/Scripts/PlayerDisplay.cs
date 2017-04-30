using UnityEngine;
using System.Collections;

using UnityEngine.UI;
/// <summary>
/// Control the display 
/// </summary>
public class PlayerDisplay : MonoBehaviour {
	public Text scoreText;
    public Text keysText;
    public Image livesImage;
    public Image jumpImage;

    public Sprite spring10, spring9, spring8, spring7, spring6, spring5, spring4, spring3, spring2, spring1, spring0;
    public Sprite lives0Sprite;
	public Sprite lives1Sprite;
	public Sprite lives2Sprite;
	public Sprite lives3Sprite;

    public void UpdateScoreText(int newScore){
		string scoreMessage = "Score = " + newScore;
		scoreText.text = scoreMessage;
	}
    //-------------------------------
    public void UpdateKeysText(int newKeys)
    {
        string keysMessage = "Keys = " + newKeys;
        keysText.text = keysMessage;
    }

    //-------------------------------
    public void UpdateLivesImage(int newLives){
		switch(newLives)
		{
		case 3:
			livesImage.sprite = lives3Sprite;
			break;
		case 2:
			livesImage.sprite = lives2Sprite;
			break;
		case 1:
			livesImage.sprite = lives1Sprite;
			break;
		case 0:
		default:
			livesImage.sprite = lives0Sprite;
			break;
		}
	}
    public void UpdateJumpImage(int secondsLeft)
    {

        if (secondsLeft >= 10)
        {
            //count++;
            jumpImage.sprite = spring10;
        }
        else if (secondsLeft >= 9)
        {
            jumpImage.sprite = spring9;
        }
        else if (secondsLeft >= 8)
        {
            jumpImage.sprite = spring8;
        }
        else if (secondsLeft >= 7)
        {
            jumpImage.sprite = spring7;
        }
        else if (secondsLeft >= 6)
        {
            jumpImage.sprite = spring6;
        }
        else if (secondsLeft >= 5)
        {
            jumpImage.sprite = spring5;
        }
        else if (secondsLeft >= 4)
        {
            jumpImage.sprite = spring4;
        }
        else if (secondsLeft >= 3)
        {
            jumpImage.sprite = spring3;
        }
        else if (secondsLeft >= 2)
        {
            jumpImage.sprite = spring2;
        }
        else if (secondsLeft >= 1)
        {
            jumpImage.sprite = spring1;
        }
        else if (secondsLeft > 0)
        {
            jumpImage.sprite = spring1;
        }
        else
        {
            jumpImage.sprite = spring0;
        }
        if (secondsLeft == -15)
        {
            //after 15 seconds, show the pink gem again so the player can try again
            GetComponent<Player>().gemPink.SetActive(true);
        }
    }
}
