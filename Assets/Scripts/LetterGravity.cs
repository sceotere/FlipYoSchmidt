using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
public class LetterGravity : MonoBehaviour, IPointerEnterHandler, ISelectHandler
{
    //Allows the letters of the game name to fall up and down depending on the player being on the "play" button
    public float gravity = 1.0f; // Gravity magnitude to be used

    public static int orientation = -1; // Gravitational orientation, switches between 1 and -1

    private void Start()
    {
        Physics.gravity = new Vector3(0.0f, -1 * gravity * 10f, 0.0f);
    }

    void FlipGravity()
    {
        orientation *= -1;
        // Not sure what gravity multiplier best syncs player acceleration with normal rigidbody acceleration
        Physics.gravity = new Vector3(0.0f, -1 * orientation * gravity * 10f, 0.0f);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        FlipGravity();
    }

    public void OnSelect(BaseEventData eventData)
    {
        //do your stuff when selected
        SceneManager.LoadScene("BetaLevels");
    }


    // Update is called once per frame
    void Update()
    {
        // Quit the game on hitting escape
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
        
    }
}
