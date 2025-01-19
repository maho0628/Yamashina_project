using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;


public class CheckExplorable : MonoBehaviour
{
    [Header("“ú’†Às‚³‚ê‚éŠÖ”")]
    [SerializeField]
    UnityEvent m_dayTime_event;
    [Header("–éÀs‚³‚ê‚éŠÖ”")]
    [SerializeField]
    UnityEvent m_night_event;
    
    


    public void CheckAndDistribute()
    {
        if (PlayerInfo.Instance.Day.isDayTime)
        {

            m_dayTime_event.Invoke();
        }
        else
        {
            m_night_event.Invoke();
        }
    }
}
