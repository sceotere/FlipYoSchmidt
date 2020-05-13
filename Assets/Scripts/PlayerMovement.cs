/*
 * Flip Yo' Schmidt
 * 
 * PlayerMovement.cs
 * Description: Script that controls player movement, gravity, and the first person camera
 * 
 * Author: Jake Gianola, Joseph Goh
 * Acknowledgements: N/A
 * Last Updated: 05/12/2020
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;

    public float speed = 12f;
    public float gravity = 1.0f; // Gravity magnitude to be used

    public static int orientation = 1; // Gravitational orientation, switches between 1 and -1
    bool isGrounded = true; // Our own isGrounded variable since we can be on the ceiling too
    float y_vel = 0; // y-axis (vertical in a 3d sense) velocity for gravity

    private void Start()
    {
        Physics.gravity = new Vector3(0.0f, -1 * gravity * 10f, 0.0f);
    }

    // Update is called once per frame
    void Update()
    {
        // Quit the game on hitting escape
        if (Input.GetKeyDown(KeyCode.Escape)) {
        Application.Quit();
        }

        // Set the isGrounded variable appropriately
        SetGrounded();

        // If the player is grounded, y_vel should be a slight groundward force
        // This helps isGrounded be consistently updated.
        if (isGrounded)
        {
            y_vel = -0.01f * orientation;
        }

        //If the player presses space, try to switch gravity
        if (Input.GetKeyDown(KeyCode.Space))
        {
            FlipGravity();
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        Vector3 move;

        // Multiply x by orientation to match camera left/right
        move = transform.right * orientation * x + transform.forward * z;

        // If the player is not currently grounded, adjust y_vel for gravity
        if (!isGrounded)
        {
            y_vel -= orientation * gravity * Time.deltaTime;
        }
        // Set move vector's y 
        move.y = y_vel;

        controller.Move(move * speed * Time.deltaTime);
    }

    void FlipGravity()
    {
        //If the player is grounded, then switch gravity orientation and reset isGrounded
        if (isGrounded)
        {
            orientation *= -1;
            // Not sure what gravity multiplier best syncs player acceleration with normal rigidbody acceleration
            Physics.gravity = new Vector3(0.0f, -1 * orientation * gravity * 10f, 0.0f);
            isGrounded = false;
        }
    }

    void SetGrounded()
    {
        // Set isGrounded value based on current orientation
        if (orientation == 1 && (controller.collisionFlags & CollisionFlags.Below) != 0)
        {
            // We're at normal orientation and touching the ground so we're grounded
            isGrounded = true;
        }
        else if (orientation == -1 && (controller.collisionFlags & CollisionFlags.Above) != 0)
        {
            // We're at reverse orientation and touching the ceiling so we're 'grounded'
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
    }
}
