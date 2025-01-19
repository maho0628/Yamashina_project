using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event_Ilast_Disply : Image_Method
{
    public Event_Manage event_Manage;
    public EventData eventData;

    // Start is called before the first frame update
    void Start()
    {     
        SetEventIlast();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void SetEventIlast()
    {
        eventData = event_Manage.eventDatas[event_Manage.now_event_num];
        //”wŒi‚ð•`ŽÊ
        PutImage(eventData.Event_Ilast);
    }
}
