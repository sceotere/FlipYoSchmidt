/* * Flip Yo' Schmidt *  * PickUp.cs * Description: Script to allow player to pick up and move objects and control their behavior *  * Author: Joseph Goh * Acknowledgements: Adapted code from Jimmy Vegas https://youtu.be/IEV64CLZra8 * Last Updated: 05/13/2020 */

using System.Collections;using System.Collections.Generic;using UnityEngine;public class PickUp : MonoBehaviour{    public Transform pickUpDestination;    public ColliderType colliderType;    public float roomHeight = 20f;    public enum ColliderType    {        Box,        Sphere,        Capsule    }    public GameEnding gameEnding;    bool destroy = false;

    private void Update()
    {
        if (!GetComponent<AudioSource>().isPlaying && destroy)
        {
            gameEnding.BallDestroyed();
            Destroy(this.gameObject);
        }
    }    private void OnMouseDown()    {        Rigidbody r = GetComponent<Rigidbody>();        Transform t = this.transform;        /*        // Temporarily turn off collider        if (colliderType == ColliderType.Sphere)        {            GetComponent<SphereCollider>().enabled = false;        }        */        // Temporarily turn off gravity and make kinematic        r.useGravity = false;        r.isKinematic = true;        // Attach it to the space in front of player        t.position = pickUpDestination.position;        t.parent = GameObject.Find("PickUpDestination").transform;    }    private void OnMouseUp()    {        Rigidbody r = GetComponent<Rigidbody>();        Transform t = this.transform;        // Detach the object from the player        t.parent = null;        /*        // Turn collider back on        if (colliderType == ColliderType.Sphere)        {            GetComponent<SphereCollider>().enabled = true;        }        */                // Turn gravity back on and make non-kinematic        r.useGravity = true;        r.isKinematic = false;    }    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Lava")
        {
            GetComponent<AudioSource>().Play();
            destroy = true;
        }
    }}