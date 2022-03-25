using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileSound : MonoBehaviour
{
    public GameObject obst;
    public GameObject explosion;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Wall" || collision.gameObject.tag == "Slab")
        {
            AudioManager.instance.Play("collision");

        }
        else if (collision.gameObject.tag == "Obstacle")
        {
            //obst.SetActive(false);
            //obst.GetComponent<Renderer>().enabled = true;
            Destroy(this.gameObject);
            GameObject expl = Instantiate(explosion, transform.position, Quaternion.identity) as GameObject;
            Destroy(expl, 1);

        }
    }
}
