using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Slingshot : MonoBehaviour
{

    static private Slingshot S;
    //fields set in the Unity Inspector pane
    [Header("Set in Inspector")]
    public float velocityMult = 8f;

    [Header("Set Dynamically")]
    public GameObject launchPoint;
    public Vector3 launchPos;
    public GameObject projectile;
    public bool aimingMode;
    private Rigidbody projectileRigidbody;
    public static int shotsTaken;
    public GameObject[] projectiles;
    public static int level;
    public GameObject Goal;

    void Start()
    {
        shotsTaken = 0;
        level = 0;
    }
    static public Vector3 LAUNCH_POS
    {
        get
        {
            if (S == null) return Vector3.zero;
            return S.launchPos;
        } 
    }

    private void Awake()
    {
        S = this;
        Transform launchPointTrans = transform.Find("LaunchPoint");  //AHhhhhh
        launchPoint = launchPointTrans.gameObject;
        launchPoint.SetActive(false);
        launchPos = launchPointTrans.position;
    }

    private void OnMouseEnter()
    {
        //Debug.Log("Slingshot:OnMouseEnter()");
        
        launchPoint.SetActive(true);
    }

    private void OnMouseExit()
    {
        //Debug.Log("Slingshot:OnMouseExit()");
        launchPoint.SetActive(false);
    }

    private void OnMouseDown()
    {
        
        // The player can only try three shots
        if (shotsTaken < 3)
        {

            //Play the sound 
            AudioManager.instance.Play("ready");

            //The player has pressed the mouse button while over SlingShot
            aimingMode = true;
            // This if statement helps me to loop thorugh the projectiles.
            // We only have three projectile so if we alreday passed three levels
            // we also have used all the projectiles and we want to use them again
            // from the first one

            //Instantiate a projectile, I use the module to keep the num from 1 to 3
            projectile = Instantiate(projectiles[level % 3]) as GameObject;
            //Start it at the launch point
            projectile.transform.position = launchPos;

            //Set it to isKinematic for now
            //projectile.GetComponent < Rigidbody > ().isKinematic = true;

            projectileRigidbody = projectile.GetComponent<Rigidbody>();
            projectileRigidbody.isKinematic = true;
        }
        else 
        {
            shotsTaken = 0;
        }
    }

    private void Update()
    {
        //If SlingShot is not in aimingMode, don't run this code
        if (!aimingMode) return; //AAAAHHHH

        //Get the current mouse position in 2D screen coordinate
        Vector3 mousePos2D = Input.mousePosition;
        mousePos2D.z = -Camera.main.transform.position.z;
        Vector3 mousePos3D = Camera.main.ScreenToWorldPoint(mousePos2D);

        //Find the delta from the launchPos to the mousePos3D
        Vector3 mouseDelta = mousePos3D - launchPos;
        //Limit mouseDelta to the radius ot the Slingshot SphereCollider
        float maxMagnitude = this.GetComponent<SphereCollider>().radius;
        if (mouseDelta.magnitude > maxMagnitude)
        {
            mouseDelta.Normalize();
            mouseDelta *= maxMagnitude;
        }

        //Move the projectile to this new position
        Vector3 projPos = launchPos + mouseDelta;
        projectile.transform.position = projPos;


        if (Input.GetMouseButtonUp(0))
        {
            
            //The mouse has been released
            aimingMode = false;
            projectileRigidbody.isKinematic = false;
            projectileRigidbody.velocity = -mouseDelta * velocityMult;
            FollowCam.POI = projectile;
            projectile = null;
            MissionDemolition.ShotFired();
            ProjectileLine.S.poi = projectile;
            //Playing shooting sound
            AudioManager.instance.Play("go");
            shotsTaken++;
            
        }

    }

}
