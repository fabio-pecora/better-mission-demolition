using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostProjectile : MonoBehaviour
{
    private float movementSpeed = 10;
    
    void Update()
    {
        //get the Input from Horizontal axis
        float horizontalInput = Input.GetAxis("Horizontal");
        //get the Input from Vertical axis
        float verticalInput = Input.GetAxis("Vertical");

        //update the position
        transform.position = transform.position + new Vector3(horizontalInput * movementSpeed * Time.deltaTime, verticalInput * movementSpeed * Time.deltaTime, 0);

        if (transform.position.y < -11) Destroy(gameObject);
    }
}
