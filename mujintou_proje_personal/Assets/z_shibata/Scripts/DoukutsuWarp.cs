using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoukutsuWarp : MonoBehaviour
{

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            EventSceneControllerBase event_controller;
            event_controller = FindObjectOfType<EventSceneControllerBase>();
            event_controller.GoCave_L();
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            EventSceneControllerBase event_controller;
            event_controller = FindObjectOfType<EventSceneControllerBase>();
            event_controller.GoCave();
        }
    }
}
