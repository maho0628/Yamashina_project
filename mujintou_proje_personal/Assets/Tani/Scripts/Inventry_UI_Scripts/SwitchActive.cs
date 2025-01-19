using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent((typeof(EventTrigger)))]
public class SwitchActive : MonoBehaviour
{
    EventTrigger trigger;
    [SerializeField]
    GameObject firstActive;
    [SerializeField]
    GameObject secondActive;

    // Start is called before the first frame update
    void Start()
    {
        trigger = GetComponent<EventTrigger>();
        trigger.triggers.Add(new EventTrigger.Entry { eventID = EventTriggerType.PointerClick });
        trigger.triggers[0].callback.AddListener((data) =>
        {
            firstActive.SetActive(!firstActive.activeSelf);
            secondActive.SetActive(!secondActive.activeSelf);
            PlayerInfo.Instance.OnVisibilityChanged();
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
