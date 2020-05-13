/* * Flip Yo' Schmidt *  * BallReceptacle.cs * Description: Script controlling game ending UI *  * Author: Joseph Goh * Acknowledgements: N/A * Last Updated: 05/13/2020 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEnding : MonoBehaviour
{
    public CanvasGroup levelClearedCanvasGroup, ballDestroyedCanvasGroup, playerDeathCanvasGroup;
    public float fadeDuration = 2.0f;

    bool levelCleared = false;
    bool ballDestroyed = false;
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
        else if (ballDestroyed)
        {
            EndLevel(ballDestroyedCanvasGroup);
        }
    }

    public void BallDestroyed()
    {
        ballDestroyed = true;
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
}
