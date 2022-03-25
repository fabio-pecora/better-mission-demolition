using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    static public bool goalMet = false;
    public bool hasPlayed = false;
    
    private void OnTriggerEnter(Collider other)
    {
        //When the trigger is hit by something
        //Check to see if it's a Projectile
        if (other.gameObject.tag == "Projectile")
        {
                
            //If so, set goalMet to true
            Goal.goalMet = true;
            Slingshot.level++;
            //Also set the alpha of the color to higher opacity
            Material mat = GetComponent<Renderer>().material;
            Color c = mat.color;
            c.a = 1;
            mat.color = c;
            //I want the sound to play only once
            //If the sound already played it is not going to play again
            if (hasPlayed == false)
            {
                AudioManager.instance.Play("goalHit");
                hasPlayed = true;
            }
        }
    }

}
