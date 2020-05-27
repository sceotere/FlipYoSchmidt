/*
 * Flip Yo' Schmidt
 * 
 * Activatable.cs
 * Description: Abstract base class for all receptacle-activatable objects
 * 
 * Author: Joseph Goh
 * Acknowledgements: N/A
 * Last Updated: 05/27/2020
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Activatable : MonoBehaviour
{
    public float speed = 1.0f;
    
    protected bool activated = false;
    protected bool perma_deactivated = false;

    // Start must be implemented in each derived class 
    // with self_transform set to GetComponent<Transform>() and
    // with an original and target location set accordingly
    protected Transform self_transform;
    protected Vector3 origLocation, targetLocation;
    protected float t = 0.0f;

    // Update is called once per frame
    protected void Update()
    {
        if (perma_deactivated && t > 0)
        {
            t -= speed * Time.deltaTime;
        }
        else if (activated && t < 1)
        {
            t += speed * Time.deltaTime;
        }
        else if (!activated && t > 0)
        {
            t -= speed * Time.deltaTime;
        }
        self_transform.position = Vector3.Lerp(origLocation, targetLocation, t);
    }

    public void Activate()
    {
        activated = true;
    }

    public void Deactivate()
    {
        activated = false;
    }

    public void PermanentDeactivate()
    {
        perma_deactivated = true;
    }
}
