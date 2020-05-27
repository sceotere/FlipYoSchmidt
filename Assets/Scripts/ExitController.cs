/*
 * Flip Yo' Schmidt
 * 
 * ExitController.cs
 * Description: Script controlling exit activation/deactivation
 * 
 * Author: Joseph Goh
 * Acknowledgements: N/A
 * Last Updated: 05/27/2020
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitController : Activatable
{
    // Start is called before the first frame update
    void Start()
    {
        self_transform = GetComponent<Transform>();
        origLocation = self_transform.position;
        targetLocation = origLocation + new Vector3(0.0f, 20.0f, 0.0f);
    }
}
