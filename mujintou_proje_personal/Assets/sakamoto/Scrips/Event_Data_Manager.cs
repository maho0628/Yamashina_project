using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event_Data_Manager : MonoBehaviour
{
    protected EventData eventData;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void setEventData(EventData evData)
    {
        eventData = evData;
    }
    public void setEvData(EventData evData)
    {
        evData = eventData;
    }
}
