using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundsEffect : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        // when the gameObject hits a slab or a wall then play the sound effect
        if(collision.gameObject.tag == "Wall")
        {

            AudioManager.instance.Play("collision");
        }
    }
}
