using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleScript : MonoBehaviour
{
    public float speed;
    float targetTime;
  
    int timeIncrese;
    float x;
    public AudioSource explosionSound;

    void Start()
    {
        x = 30;
        timeIncrese = 0;
        speed = 1;
        explosionSound = GetComponent<AudioSource>();
        targetTime = 11;
   
    }

    // Update is called once per frame
    void Update()
    {
        
        float y = Mathf.PingPong(Time.time * speed, 1) * 18 - 3;
        this.transform.position = new Vector3(x, y, 0);
            
        targetTime -= Time.deltaTime;
       

        //Changing the x position everytime the timer ends (7 seconds)
        if (CountdownTimer.secondsLeft <= 0)
        {
            // from x = -6, to x = 30
            x = Random.Range(10f, 30f);
        }
        //choosing how the movement of the obstacle get affected by the time 
        // in order to keep the game possible to play
        if (targetTime <= 0)
        {
            timeIncrese++;
            if (timeIncrese == 1)
            {
                targetTime = 20;
                speed += 1;
            }
            else if (timeIncrese == 2)
            {
                targetTime = 30;
                speed += 1;
            }
            else
            {
                speed += 1;
                targetTime = 30;
            }
         
        }

    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Projectile")
        {
            AudioManager.instance.Play("explosion");
        }
    }
}
