/*
 * Flip Yo' Schmidt
 * 
 * MouseLook.cs
 * Description: Script that controls the first person camera
 * 
 * Author: Jake Gianola
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

    //Used to smoothly change the gravity by keeping track of the frames
    int framecount = 0;

    //Used to see if the player is flipped so that the camera can adjust properly
    bool flipped = false;

    //Input from the mouse (X axis)
    float mouseX;

    //Gravity orientation for the player. -1 is upside down, 1 is normal
    float orientation;

    // Start is called before the first frame update
    void Start()
    {
        //Locks the cursor into the middle of the screen and hides it on game startup
        Cursor.lockState = CursorLockMode.Locked;
    }

    void FixedUpdate()
    {
        //Get player orientation from the PlayerMovement script
        orientation = PlayerMovement.orientation;

        //START OF SMOOTH CAMERA FLIPPER: flipping smoothly upside down in 36 frames.
        //Gravity is normal, player wants it upside down.
        if (orientation < 0 && !flipped)
        {
            framecount = framecount + 1;
            Vector3 cameraRotate = new Vector3(0, 0, 10 * framecount);
            transform.localRotation = Quaternion.Euler(-xRotation, 0.0f, (5.0f * framecount));
            //setup camera position
            if(framecount == 1)
            {

                transform.position = new Vector3(transform.position.x, (transform.position.y - 3.2f), transform.position.z);
            }

            if (framecount > 35)
            {
                flipped = true;
                framecount = 0;
            }

        }
        //Gravity is upside down, player wants it back to normal
        else if (orientation > 0 && flipped)
        {
            framecount = framecount + 1;
            //Start changing the x rotation halfway through changing the z rotation to avoid weird camera snaps
            if (framecount > 17)
            {
                transform.localRotation = Quaternion.Euler(xRotation, 0f, 180f - (5.0f * framecount));
            }

            //otherwise just change the z rotation only
            else
            {
                transform.localRotation = Quaternion.Euler(xRotation, 0f, 180f - (5.0f * framecount));
            }
          
            //setup camera position
            if (framecount == 1)
            {
                transform.position = new Vector3(transform.position.x, (transform.position.y + 3.2f), transform.position.z);
            }
          
            if (framecount > 35)
            {
                flipped = false;
                framecount = 0;
            }
        }
        //END OF SMOOTH CAMERA FLIPPER
    }

    // Update is called possibly multiples times per frame
    void Update()
    {

        //Scales based on mouse sensitivity and per frame
        mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;


        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); //Gives the camera a human perspective

        //if player is done flipping upside down, make sure player can still look up and down correctly
        if (orientation < 0 && flipped)
        {
            playerbody.Rotate(Vector3.down * mouseX);
            transform.localRotation = Quaternion.Euler(-xRotation, 0f, 180f);
        }
        else if (orientation > 0 && !flipped)
        {
            playerbody.Rotate(Vector3.up * mouseX);
            transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        }
    }
}

