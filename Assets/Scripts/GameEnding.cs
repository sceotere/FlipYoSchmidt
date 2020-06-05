/*
 * Flip Yo' Schmidt
 * 
 * UIController.cs
 * Description: Script controlling UI and game end events
 * 
 * Author: Joseph Goh
 * Acknowledgements: N/A
 * Last Updated: 05/13/2020
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEnding : MonoBehaviour
{
    public CanvasGroup levelClearedCanvasGroup, playerDeathCanvasGroup;
    public float fadeDuration = 2.0f;

    bool levelCleared = false;
    bool playerDeath = false;

    float timer = 0.0f;

    // Update is called once per frame
    void Update()
    {
        if (levelCleared)
        {
            EndLevel(levelClearedCanvasGroup);
        }
        else if (playerDeath)
        {
            EndLevel(playerDeathCanvasGroup);
        }
    }

    public void LevelCleared()
    {
        levelCleared = true;
    }

    public void PlayerDeath()
    {
        playerDeath = true;
    }

    private void EndLevel(CanvasGroup canvasGroup)
    {
        timer += Time.deltaTime;
        canvasGroup.alpha = timer / fadeDuration;
    }

    public void ClearCanvas()
    {
        levelCleared = false;
        playerDeath = false;
        timer = 0f;
        levelClearedCanvasGroup.alpha = 0f;
        playerDeathCanvasGroup.alpha = 0f;
    }
}
