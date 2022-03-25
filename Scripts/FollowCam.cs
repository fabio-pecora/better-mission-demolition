using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class FollowCam : MonoBehaviour
{
    static private MissionDemolition S;  //a private Singleton

    static public GameObject POI; //the static point of interest

    [Header("Set Dynamically")]
    public float camZ; //The desired Z pos of the camera

    [Header("Set in Inspector")]
    public float easing = 0.05f;   //5% camera movement (Weir)
    public Vector2 minXY = Vector2.zero;

    private void Awake()
    {
        camZ = this.transform.position.z;
    }

    private void FixedUpdate()
    {

        //if (POI == null) return;


        //Get the position of the POI
        // destination = POI.transform.position;

        Vector3 destination;
        //If there is no POI, return to P[ 0, 0, 0]
        if (POI == null)
        {
            destination = Vector3.zero;
        }
        else
        {
            //Get the position of the POI            
            destination = POI.transform.position;
            //If POI is a projectile, check to see if it's at rest
            if (POI.tag == "Projectile")
            {
                //if it is sleeping (that is, not moving)
                if (POI.GetComponent<Rigidbody>().IsSleeping())
                {

                    //return to default view
                    POI = null;
                    //in the next update
                    return;
                }




                if(Slingshot.shotsTaken == 3)
                {
                    if (POI.GetComponent<Rigidbody>().IsSleeping())
                    {
                        SceneManager.LoadScene(sceneName: "GameOverScene");
                    }
                }





            }

        }
        //Limit the X and Y to minimum values
        destination.x = Mathf.Max(minXY.x, destination.x);
        destination.y = Mathf.Max(minXY.y, destination.y);
        //Interpolate from the current Camera position toward destination
        destination = Vector3.Lerp(transform.position, destination, easing);
        //Force destination.z to be camZ to keep the camera far enough away
        destination.z = camZ;
        //Set the camera to the destination
        transform.position = destination;
        Camera.main.orthographicSize = destination.y + 10; //expand to keep the ground in view
    }
}
