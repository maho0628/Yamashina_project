using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Diagnostics.Tracing;

public class checkText: MonoBehaviour
{
    //inspector field
    [SerializeField]
    List<EventDatas> EventList;
    [SerializeField] TextControl textControl;


    void Start()
    {
        for (int i = 0; i < EventList.Count; i++)
        {
            textControl.AddTextData(EventList[i].scene_id.ToString());
            textControl.AddTextData(EventList[i].event_title.ToString() );
            textControl.AddTextData(EventList[i].main_text.ToString()       );
            for (int j = 0; j < EventList[i].results.Count; j++)
            {
                textControl.AddTextData(EventList[i].results[j].choise_text.ToString());
                textControl.AddTextData(EventList[i].results[j].result_text.ToString()  );
            }
        }
    }


}
