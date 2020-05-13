/*
 * Flip Yo' Schmidt
 * 
 * MouseLook.cs
 * Description: Script that controls the first person camera
 * 
 * Author: Jake Gianola, Joseph Goh
 * Acknowledgements: Adapted from Unity First Person Movement Tutorial https://youtu.be/_QajrabyTJc 
 * Last Updated: 05/12/2020
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    //Sensitivity of the mouse for determining look speed
    public float mouseSensitivity = 100f;

    //Player view
    public Transform playerbody;

    //Used to allow the player to look up and down
    float xRotation = 0f;

    //Used to see if the player is flipped so that the camera can adjust properly
    bool flipped = false;

    // Start is called before the first frame update
    void Start()
    {
        //Locks the cursor into the middle of the screen and hides it on game startup
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        //Get player orientation from the PlayerMovement script
        float orientation = PlayerMovement.orientation;

        //Flip camera position based on gravity

        //Gravity is reversed
        if (orientation < 0 && !flipped)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - 2.0f, transform.position.z);
            flipped = true;
        }
        //Gravity is normal
        else if (orientation > 0 && flipped) {
            transform.position = new Vector3(transform.position.x, transform.position.y + 2.0f, transform.position.z);
            flipped = false;
        }

        //Scales based on mouse sensitivity and per frame
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); //Gives the camera a human perspective
        if (orientation < 0)
        {
            transform.localRotation = Quaternion.Euler(180.0f + xRotation, 180f, 0f);
            playerbody.Rotate(Vector3.down * mouseX);
        }
        else
        {
            transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
            playerbody.Rotate(Vector3.up * mouseX);
        }
    }
}

