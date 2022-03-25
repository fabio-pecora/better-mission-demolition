using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public static float currTime;
    public static float targetTime = 10f;

    void Start()
    {
        currTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        targetTime -= Time.deltaTime;
        if (Slingshot.shotsTaken == 3)
        {
            currTime += Time.deltaTime;
        }
    }
    public static bool onTimerEnd()
    {
        if (currTime == targetTime)
        {
            return true;
        }
        else return false;
    }
}
