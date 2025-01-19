using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class BackHomeButton : MonoBehaviour
{
    EventSceneControllerBase event_controller;
    void Start()
    {
        if(!GameObject.FindWithTag("GameController").TryGetComponent<EventSceneControllerBase>(out event_controller))
        {
            Debug.LogError("GameController doesn't have EventSceneControllerBase");
        }
        GetComponent<Button>().onClick.AddListener(event_controller.BackHome);
        
    }
}
