using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;


public class GameOverMenu : MonoBehaviour
{
    void Start()
    {
        
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(sceneName: "Title_Scene");
    }
    public void QuitGame()
    {
        Debug.Log("QUIT");
        // to quit the game
        Application.Quit();
    }
}
