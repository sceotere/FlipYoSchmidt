/*
 * Flip Yo' Schmidt
 * 
 * BallReceptacle.cs
 * Description: Script controlling BallReceptacles' activation of the linked_object
 * 
 * Author: Joseph Goh
 * Acknowledgements: N/A
 * Last Updated: 05/27/2020
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallReceptacle : MonoBehaviour
{
    public Activatable linked_object;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ball"))
        {
            linked_object.Activate();
            GetComponent<AudioSource>().Play();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Ball"))
        {
            linked_object.Deactivate();
        }
    }
}
