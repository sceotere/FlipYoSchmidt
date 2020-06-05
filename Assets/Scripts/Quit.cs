using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class Quit : MonoBehaviour, ISelectHandler
{
    public void OnSelect(BaseEventData eventData)
    {
        //do your stuff when selected
        Application.Quit();
    }
}
