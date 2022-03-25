using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // method to start the game
    public void PlayGame()
    {
        // loading the scene that has one more index than the one we are in the build setting
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        Debug.Log("QUIT");
        // to quit the game
        Application.Quit();
    }
}
