using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* This code is a slightly modified version of the code found in this
Unity First Person Movement Tutorial on Youtube: 
https://www.youtube.com/watch?v=_QajrabyTJc 
*/

public class MouseLook : MonoBehaviour
{
    //Sensitivity of the mouse for determining look speed
    public float mouseSensitivity = 100f;

    //Player view
    public Transform playerbody;

    //Used to allow the player to look up and down
    float xRotation = 0f;

    // Start is called before the first frame update
    void Start()
    {
        //Locks the cursor into the middle of the screen and hides it on game startup
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        //Scales based on mouse sensitivity and per frame
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        //Logic for first person camera to look up and down
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); //Gives the camera a human perspective
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerbody.Rotate(Vector3.up * mouseX);
    }
}
