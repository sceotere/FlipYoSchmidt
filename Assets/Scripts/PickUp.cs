/*
 * Flip Yo' Schmidt
 * 
 * PickUp.cs
 * Description: Script to allow player to pick up and move objects and control their behavior
 * 
 * Author: Joseph Goh
 * Acknowledgements: Adapted code from Jimmy Vegas https://youtu.be/IEV64CLZra8
 * Last Updated: 05/27/2020
 */

using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class PickUp : MonoBehaviour
{       
    public Transform pickUpDestination;

    public GameEnding gameEnding;

    bool respawn = false;

    Rigidbody r;
    Transform t;
    UnityEngine.Vector3 orig_position;

    AudioSource[] audioSources;

    private void Start()
    {
        r = GetComponent<Rigidbody>();
        t = this.transform;
        orig_position = t.position;
        t.parent = null;

        audioSources = GetComponents<AudioSource>();
    }

    private void Update()
    {
        if (!audioSources[0].isPlaying && respawn)
        {
            r.useGravity = true;
            r.isKinematic = false;
            RespawnBall();
        }
    }

    private void OnMouseDown()
    {
        // Temporarily turn off gravity
        r.useGravity = false;

        // Attach it to the space in front of player
        r.velocity = UnityEngine.Vector3.zero;
        if (t.parent == null)
        {
            t.position = pickUpDestination.position;
        }
        t.parent = GameObject.Find("PickUpDestination").transform;
    }

    private void OnMouseUp()
    {

        // Detach the object from the player
        t.parent = null;
        
        // Turn gravity back on
        r.useGravity = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Lava"))
        {
            r.useGravity = false;
            r.isKinematic = true;
            audioSources[0].Play();
            respawn = true;
        }
    }

    void RespawnBall()
    {
        t.position = orig_position;
        respawn = false;
        audioSources[1].Play();
    }
}
