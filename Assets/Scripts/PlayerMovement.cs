using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;

    public float speed = 12f;
    public float gravity = 1.0f; // Gravity magnitude to be used

    int orientation = 1; // Gravitational orientation, switches between 1 and -1
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

        // If the player is grounded, y_vel should be a slight groundward force
        // This helps isGrounded be consistently updated.
        if (isGrounded)
        {
            y_vel = -0.01f * orientation;
        }

        //If the player presses space and we are grounded, then switch gravity orientation and reset isGrounded
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            orientation *= -1;
            // Not sure what gravity multiplier best syncs player acceleration with normal rigidbody acceleration
            Physics.gravity = new Vector3(0.0f, -1 * orientation * gravity * 10f, 0.0f);
            isGrounded = false;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        // If the player is not currently grounded, adjust y_vel for gravity
        if (!isGrounded)
        {
            y_vel -= orientation * gravity * Time.deltaTime;
        }
        // Set move vector's y 
        move.y = y_vel;

        controller.Move(move * speed * Time.deltaTime);
    }
}
