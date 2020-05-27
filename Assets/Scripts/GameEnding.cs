/* * Flip Yo' Schmidt *  * BallReceptacle.cs * Description: Script controlling game ending UI *  * Author: Joseph Goh * Acknowledgements: N/A * Last Updated: 05/13/2020 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEnding : MonoBehaviour
{
    public CanvasGroup levelClearedCanvasGroup, ballDestroyedCanvasGroup, playerDeathCanvasGroup;
    public float fadeDuration = 2.0f;

    public AudioSource victoryAudio, loseAudio;

    bool levelCleared = false;
    bool ballDestroyed = false;
    bool playerDeath = false;
    bool hasAudioPlayed = false;

    float timer = 0.0f;

    // Update is called once per frame
    void Update()
    {
        if (levelCleared)
        {
            EndLevel(levelClearedCanvasGroup, victoryAudio);
        }
        else if (playerDeath)
        {
            EndLevel(playerDeathCanvasGroup, loseAudio);
        }
        else if (ballDestroyed)
        {
            EndLevel(ballDestroyedCanvasGroup, loseAudio);
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

    private void EndLevel(CanvasGroup canvasGroup, AudioSource audioSource)
    {
        if (!hasAudioPlayed)
        {
            audioSource.Play();
            hasAudioPlayed = true;
        }

        timer += Time.deltaTime;
        canvasGroup.alpha = timer / fadeDuration;
    }
}
