using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(EventTrigger))]
public class PanelTrigger : MonoBehaviour
{
    [SerializeField]
    PrefabManager.PrefabKey key;
    

    int panel_sort_order = 1;
    Canvas panels_parent_canvas = null;

    PrefabManager manager;
    EventTrigger eventTrigger;
    private void Awake()
    {
        if (PrefabManager.InstanceNullable == null)
        {
            Debug.LogError("PrefabManager is Null");
        }
        manager = PrefabManager.Instance;
        foreach (var item in GameObject.FindObjectsByType<Canvas>(sortMode: FindObjectsSortMode.None))
        {
            print(item.gameObject.name);
            if(item.sortingOrder == panel_sort_order)
            {
                panels_parent_canvas = item;
            }
        }


        eventTrigger = GetComponent<EventTrigger>();
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerClick;
        entry.callback.AddListener((data)=>ActivatePanel());
        eventTrigger.triggers.Add(entry);

    }

    void ActivatePanel()
    {
        var go = manager.GetPrefabInstance(key);
        go.transform.SetParent(panels_parent_canvas.transform);
    }

}
