/*
 * Flip Yo' Schmidt
 * 
 * PlayerMovement.cs
 * Description: Script that controls player movement, gravity, and interactions with environment stuff
 * 
 * Author: Jake Gianola, Joseph Goh
 * Acknowledgements: N/A
 * Last Updated: 05/26/2020
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public GameEnding gameEnding;

    public float speed = 12f;
    public float gravity = 1.0f; // Gravity magnitude to be used

    public static int orientation = 1; // Gravitational orientation, switches between 1 and -1
    bool isGrounded = true; // Our own isGrounded variable since we can be on the ceiling too
    float y_vel = 0; // y-axis (vertical in a 3d sense) velocity for gravity

    Transform t;
    Vector3 checkpoint; // Checkpoint position

    bool hasPlayed = false;

    private void Start()
    {
        t = GetComponent<Transform>();
        checkpoint = t.position;
        Physics.gravity = new Vector3(0.0f, -1 * gravity * 10f, 0.0f);
    }

    // Update is called once per frame
    void Update()
    {
        // Set the isGrounded variable appropriately
        SetGrounded();
        // If the player is grounded, y_vel should be a slight groundward force
        // This helps isGrounded be consistently updated.
        if (isGrounded)
        {
            y_vel = -0.01f * orientation;
        }

        // Quit the game on hitting escape
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
        // Reset player position to checkpoint on hitting R
        else if (Input.GetKeyDown(KeyCode.R))
        {
            t.position = checkpoint;
            gameEnding.ClearCanvas();
        }
        else
        {
            //If the player presses space, try to switch gravity
            if (Input.GetKeyDown(KeyCode.Space))
            {
                FlipGravity();
            }
            float x;
            float z;

            //if gravity is flipped, reverse controls to make it normal for player
            x = Input.GetAxis("Horizontal");
            z = Input.GetAxis("Vertical");

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

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.CompareTag("Lava"))
        {
            if (!hasPlayed)
            {
                GetComponent<AudioSource>().Play();
                hasPlayed = true;
            }
            gameEnding.PlayerDeath();
        }
        else if (hit.gameObject.CompareTag("Checkpoint"))
        {
            checkpoint = hit.gameObject.transform.position;
            checkpoint.y = 10f;
        }
        else if (hit.gameObject.CompareTag("Finish"))
        {
            if (!hasPlayed)
            {
                hit.gameObject.GetComponent<AudioSource>().Play();
                hasPlayed = true;
            }
            gameEnding.LevelCleared();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Finish"))
        {
            if (!hasPlayed)
            {
                other.gameObject.GetComponent<AudioSource>().Play();
                hasPlayed = true;
            }
            gameEnding.LevelCleared();
        }
        else if (other.gameObject.CompareTag("Checkpoint"))
        {
            checkpoint = other.gameObject.transform.position;
            checkpoint.y = 10f;
        }
    }
}
