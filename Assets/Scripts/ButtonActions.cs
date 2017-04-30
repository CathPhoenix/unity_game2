using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ButtonActions : MonoBehaviour
{
   
	/// <summary>
    /// Buttons on the game over and game won screen to load the welcome screen
    /// </summary>
    public void BUTTON_LOAD_SCENE_WELCOME()
    {
		SceneManager.LoadScene("scene0_Welcome");
	}

	/// <summary>
    /// button on the welcome screen to begin level one
    /// </summary>
    public void BUTTON_LOAD_SCENE_LEVEL1_PLAYING()
    {
        SceneManager.LoadScene("scene2_Level1Playing");
	}
    public void BUTTON_QUIT()
    {
        Application.Quit();
    }
}

