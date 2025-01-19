using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Botton_cave1 : MonoBehaviour
{
    Button button;
    EventSceneControllerBase event_controller;
    void Start()
    {
        button = GetComponent<Button>();
        button.interactable = PlayerInfo.Instance.Inventry.GetItemAmount(Items.Item_ID.item_special_flash) >= 1;

        event_controller = FindObjectOfType<EventSceneControllerBase>();
        GetComponent<Button>().onClick.AddListener(event_controller.GoCave_L);
    }
}
