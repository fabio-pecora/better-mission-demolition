using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CountdownTimer : MonoBehaviour
{
    public static int secondsLeft = 7;
    public bool takingAway = false;
    public GameObject textDisplay;

    void Start()
    {
        textDisplay.GetComponent<Text>().text = "TELEPORT: 00:0" + secondsLeft;
        
        textDisplay.GetComponent<Text>().color = Color.green;
        
    }

    void Update()
    {

        if (secondsLeft < 7.01 && secondsLeft >= 5.01)
        {
            textDisplay.GetComponent<Text>().color = Color.green;
        }
        else if (secondsLeft < 5.01 && secondsLeft >= 2.01)
        {
            textDisplay.GetComponent<Text>().color = Color.yellow;
        }
        else
        {
            textDisplay.GetComponent<Text>().color = Color.red;
        }
        if (takingAway == false && secondsLeft > 0)
        {
            StartCoroutine(TimerTake());
        }

        if(secondsLeft <= 0)
        {
            secondsLeft = 7;
        }
    }

    IEnumerator TimerTake()
    {
        takingAway = true;
        yield return new WaitForSeconds(1);
        secondsLeft -= 1;
        textDisplay.GetComponent<Text>().text = "TELEPORT: 00:0" + secondsLeft;
        takingAway = false;

    }
}
