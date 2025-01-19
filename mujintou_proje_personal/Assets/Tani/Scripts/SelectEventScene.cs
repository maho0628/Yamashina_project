using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[System.Serializable]
class SceneTriggerPair
{
    public SceneObject scene;
    public Sprite image_normal;
    public Sprite image_selected;
    public EventTrigger eventTrigger;
}

public class SelectEventScene : MonoBehaviour
{
    [SerializeField]
    List<SceneTriggerPair> pairs;
    
    
    
    void Start()
    {
        for (int i = 0; i < pairs.Count; i++)
        {
            EventTrigger.Entry entry = new EventTrigger.Entry();
            entry.eventID = EventTriggerType.PointerDown;

        }
    }

    
    void Update()
    {
        
    }
}
