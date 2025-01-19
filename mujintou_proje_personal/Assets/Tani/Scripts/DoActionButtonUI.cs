using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DoActionButtonUI : MonoBehaviour
{
    Button button;
    Button.ButtonClickedEvent button_event;
    void Start()
    {
        button = GetComponent<Button>();
        button_event = new Button.ButtonClickedEvent();
        button_event.AddListener(ONCLICKED);
        button.onClick = button_event;
        print(button.onClick.GetPersistentEventCount());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ONCLICKED()
    {
        print("click");
    }
}
