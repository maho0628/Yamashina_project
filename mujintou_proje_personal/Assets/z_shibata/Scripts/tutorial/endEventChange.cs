using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class endEventChange : MonoBehaviour
{
    [SerializeField] EventDatas newEndEventData;
    void Start()
    {
        FindAnyObjectByType(typeof(EventPanelBase)).GetComponent<EventPanelBase>().endEventData = newEndEventData;
    }


    void SetEndEventData()
    {
        var eventPanelBase = GameObject.FindAnyObjectByType(typeof(EventPanelBase)) as EventPanelBase;
        if (eventPanelBase != null)
        {
            eventPanelBase.endEventData = newEndEventData;
        }
        else
        {
            Debug.LogError("EventPanelBase not found");
        }
    }

}
