using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;

    public float speed = 12f;

    public float gravity = 1.0f;

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);

        //If the player presses space, switch gravity
        if(Input.GetKeyDown(KeyCode.Space)){
            gravity = -1 * gravity;
        }
    }

    // Update is called at least once per frame
    void FixedUpdate()
    {
        //Account for gravity
        Physics.gravity = new Vector3(0.0f, -1 * gravity, 0.0f); 
        Vector3 grav = (-transform.up * gravity);
        controller.Move(grav * speed * Time.deltaTime);
    }
}
