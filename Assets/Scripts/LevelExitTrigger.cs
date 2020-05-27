/*
 * Flip Yo' Schmidt
 * 
 * LevelExitTrigger.cs
 * Description: Script to permanently deactivate level exits once you leave the level
 * 
 * Author: Joseph Goh
 * Acknowledgements: N/A
 * Last Updated: 05/27/2020
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelExitTrigger : MonoBehaviour
{
    public ExitController levelExit;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            levelExit.speed *= 2;
            levelExit.PermanentDeactivate();
        }
    }
}
