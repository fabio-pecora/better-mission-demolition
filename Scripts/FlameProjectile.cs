using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameProjectile : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        //Every object of the castle that the projectile hits, is going to disappear 
        if (collision.gameObject.tag == "Slab" || collision.gameObject.tag == "Wall")
        {
            AudioManager.instance.Play("collision");

            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.tag == "Obstacle")
        {
            Destroy(this.gameObject);
            AudioManager.instance.Play("explosion");

        }
    }
}
