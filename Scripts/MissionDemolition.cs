using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public enum GameMode
{
    idle,
    playing,
    levelEnd
}

public class MissionDemolition : MonoBehaviour
{
    public float targetTime;
    static private MissionDemolition S;  //a private Singleton
    [Header("Set in Inspector")]
    public Text uitLevel;  //The UIText_Level Text
    public Text uitShots; //The UIText_Shots Text
    public Text uitButton; //The Text on UIButton_View
    public Vector3 castlePos; //The place to put castles
    public GameObject[] castles;
    [Header("Set Dynamically")]
    public int level; //The current level
    public int levelMax;  //The number of levels
    public int shotsTaken;  // The number of shots
    public GameObject castle;  //The current castle
    public GameMode mode = GameMode.idle;
    public string showing = "Show Slingshot"; //FollowCam mode

    // Start is called before the first frame update
    void Start()
    {
        S = this;  //define the singleton

        level = 0;
        levelMax = castles.Length;
        StartLevel();
    }

    void StartLevel()
    {
        //Get rid of th eold castle if one exists
        if (castle != null)
        {
            Destroy(castle);
        }

        //Destroy old projectiles if they exist
        GameObject[] gos = GameObject.FindGameObjectsWithTag("Projectile");
        foreach(GameObject pTemp in gos)
        {
            Destroy(pTemp);
        }
        //Every time I instantiate a new castle I want the shots to go back to 0
        S.shotsTaken = 0;
        //Instantiate the new castle
        castle = Instantiate<GameObject>(castles[level]);
        castle.transform.position = castlePos;
        

        //Reset the camera
        SwitchView("Show Both");
        ProjectileLine.S.Clear();

        //Reset the goal
        Goal.goalMet = false;

        UpdateGUI();

        mode = GameMode.playing;
    }

    void UpdateGUI()
    {
        //Show the data in the GUITexts
        uitLevel.text = "Level: " + (level + 1) + " of " + levelMax;
        uitShots.text = "Shots Taken: " + shotsTaken;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateGUI();
        // After the third shot the projectile has 5 seconds to hit the goal 
        // or the game will end
        if (S.shotsTaken == 3)
        {
            //Decrementing timer
            targetTime -= Time.deltaTime;
            if (targetTime <= 0.0f)
            {
                // If we didn't hit the goal
                if (Goal.goalMet == false)
                {
                    SceneManager.LoadScene(sceneName: "GameOverScene");
                    AudioManager.instance.Play("YouLost");
                }
            }
        }

        if(level == 7 && Goal.goalMet == true)
        {
            SceneManager.LoadScene(sceneName: "");

        }
        //Check for level end
        if ( (mode == GameMode.playing) && Goal.goalMet)
        {
            //Change mode to stop checking for level end
            mode = GameMode.levelEnd;
            //Zoom out
            SwitchView("Show Both");
            //Start the next level in two seconds
            Invoke("NextLevel", 2f);
        }
    }

    void NextLevel()
    {
        level++;
        if (level == levelMax)
        {
            level = 0;
        }
        StartLevel();
    }

    public void SwitchView( string eView = "")
    {
        if (eView == "")
        {
            eView = uitButton.text;
        }
        showing = eView;
        switch (showing)
        {
            case "Show Slingshot":
                FollowCam.POI = null;
                uitButton.text = "Show Castle";
                break;
            case "Show Castle":
                FollowCam.POI = S.castle;
                uitButton.text = "Show Both";
                break;
            case "Show Both":
                FollowCam.POI = GameObject.Find("ViewBoth");
                uitButton.text = "Show Slingshot";
                break;
        }
    }

    // Static method that allows cods anywhere to increment shotsTaken 
    
    public static void ShotFired()
    {
        S.shotsTaken++;
    }
}
