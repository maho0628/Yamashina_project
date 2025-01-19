using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class StatusScale : MonoBehaviour
{

    [SerializeField] float scaler = 1.5f;
    [SerializeField] int flame = 6;

    float scale_per_flame = 0;
    void Start()
    {

        {
            EventTrigger eventTrigger = gameObject.GetComponent<EventTrigger>();
            EventTrigger.Entry entry = new EventTrigger.Entry();
            EventTrigger.Entry entry1 = new EventTrigger.Entry();
            entry.eventID = EventTriggerType.PointerEnter;
            entry1.eventID = EventTriggerType.PointerExit;
            entry.callback.AddListener((data) => { enter(); });
            entry1.callback.AddListener((data) => { exit(); });
            eventTrigger.triggers.Add(entry);
            eventTrigger.triggers.Add(entry1);
        }

        scale_per_flame = (scaler - 1) / flame;
    }


    
    void enter() {
        StartCoroutine("ScaleUp");
    }
    void exit() { 
        StartCoroutine("ScaleDown");

    }

    IEnumerator ScaleUp()
    {
        float scale = 1;
        while (true)
        {
            scale += scale_per_flame;
            //Debug.Log($"{scale}");
            transform.localScale = new Vector3(scale, scale, 1);
            if(scale > scaler)
            {
                transform.localScale = new Vector3(scaler, scaler, 1);
                yield break;
            }
            yield return null;
        }


    }
    IEnumerator ScaleDown()
    {
        float scale = scaler;
        while (true)
        {
            scale -= scale_per_flame;
            transform.localScale = new Vector3(scale, scale, 1);
            if (scale -1 < 0)
            {
                transform.localScale = new Vector3(1, 1, 1);
                yield break;
            }
            yield return null;
        }
    }
}
