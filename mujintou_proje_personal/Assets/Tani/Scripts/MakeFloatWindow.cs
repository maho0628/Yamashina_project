using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(EventTrigger))]
public class MakeFloatWindow : MonoBehaviour
{
    [SerializeField]
    protected GameObject window;
    [SerializeField]
    protected Vector3 offset = Vector3.zero;
    [SerializeField]
    protected int canvasOrder = 0;

    protected GameObject createdObject = null;

    protected virtual void Awake()
    {
        EventTrigger eventTrigger = GetComponent<EventTrigger>();
        EventTrigger.Entry[] entries = new EventTrigger.Entry[2];
        entries[0].eventID = EventTriggerType.PointerEnter;
        entries[1].eventID = EventTriggerType.PointerExit;

        entries[0].callback.AddListener(_ =>
        {
            if (createdObject) return;

            GameObject c_obj = new GameObject("FloatingWindowCanvas");
            Canvas c = c_obj.AddComponent<Canvas>();
            c_obj.AddComponent<CanvasRenderer>();
            c_obj.AddComponent<CanvasScaler>();
            c.overrideSorting = true;
            c.sortingOrder = canvasOrder;
            createdObject = Instantiate(window, c_obj.transform);
            createdObject.transform.localPosition = offset;

        });
        entries[1].callback.AddListener(_ =>
        {
            if (!createdObject) return;
            Destroy(createdObject.transform.parent.gameObject);
            createdObject = null;

        });

        for (int i = 0; i < entries.Length; i++)
        {
            eventTrigger.triggers.Add(entries[i]);
        }

    }



}
