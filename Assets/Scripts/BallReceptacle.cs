/*
 * Flip Yo' Schmidt
 * 
 * BallReceptacle.cs
 * Description: Script controlling Ball Receptacles and their interactions with doors
 * 
 * Author: Joseph Goh
 * Acknowledgements: N/A
 * Last Updated: 05/13/2020
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallReceptacle : MonoBehaviour
{
    public GameObject door;
    public float doorSpeed = 1.0f;

    Vector3 origLocation, targetLocation, currentLocation;
    bool activated = false;

    float t = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        origLocation = door.transform.position;
        currentLocation = origLocation;
        targetLocation = origLocation + new Vector3(0.0f, 20.0f, 0.0f);
    }

    // Update is called once per frame
    private void Update()
    {
        if (t < 1)
        {
            t += doorSpeed * Time.deltaTime;
            if (activated)
            {
                door.transform.position = Vector3.Lerp(currentLocation, targetLocation, t);
            }
            else
            {
                door.transform.position = Vector3.Lerp(currentLocation, origLocation, t);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ball"))
        {
            activated = true;
            t = 0.0f;
            currentLocation = door.transform.position;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Ball"))
        {
            activated = false;
            t = 0.0f;
            currentLocation = door.transform.position;
        }
    }
}
