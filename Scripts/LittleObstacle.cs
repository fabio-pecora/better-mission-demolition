using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LittleObstacle : MonoBehaviour
{
    public float speed;
    public AudioSource explosionSound;
  

    void Start()
    {
        speed = 2;
        explosionSound = GetComponent<AudioSource>();
    }

    void Update()
    {
        // Changing bother x and y in order to do an oblique ping pong
        float x = Mathf.PingPong(Time.time * speed, 1) * 6 - 3;
        float y = Mathf.PingPong(Time.time * speed, 1) * 13 - 3;
        this.transform.position = new Vector3(x, y, 0);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Projectile")
        {
            AudioManager.instance.Play("explosion");
        }
    }
}
