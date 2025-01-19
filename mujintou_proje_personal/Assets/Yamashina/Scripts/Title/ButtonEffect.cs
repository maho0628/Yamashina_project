using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class ButtonEffect : MonoBehaviour
{
    [SerializeField] Button b_Start;
    [SerializeField] Button b_Continue;
    [SerializeField] Button b_Option;
    [SerializeField] Button b_Credit;
    [SerializeField] Button b_Quit;
   

    [SerializeField] GameObject[] b_Image;
    // Start is called before the first frame update
    void Start()
    {

         EventTrigger trigger1 = b_Start.gameObject.AddComponent<EventTrigger>();
        trigger1.triggers.Add(new EventTrigger.Entry { eventID = EventTriggerType.PointerEnter });
        trigger1.triggers[0].callback.AddListener((data) => { OnRouteEnter(b_Start); });
        trigger1.triggers.Add(new EventTrigger.Entry { eventID = EventTriggerType.PointerExit });
        trigger1.triggers[1].callback.AddListener((data) => { OnRouteExit(b_Start); });

        EventTrigger trigger2 = b_Continue.gameObject.AddComponent<EventTrigger>();
        trigger2.triggers.Add(new EventTrigger.Entry { eventID = EventTriggerType.PointerEnter });
        trigger2.triggers[0].callback.AddListener((data) => { OnRouteEnter(b_Continue); });
        trigger2.triggers.Add(new EventTrigger.Entry { eventID = EventTriggerType.PointerExit });
        trigger2.triggers[1].callback.AddListener((data) => { OnRouteExit(b_Continue); });
        if (!DataManager.DoesSaveExist())
        {
            trigger2.triggers[0].callback.RemoveAllListeners();
            trigger2.triggers[1].callback.RemoveAllListeners();

        }
        EventTrigger trigger3 = b_Option.gameObject.AddComponent<EventTrigger>();
        trigger3.triggers.Add(new EventTrigger.Entry { eventID = EventTriggerType.PointerEnter });
        trigger3.triggers[0].callback.AddListener((data) => { OnRouteEnter(b_Option); });
        trigger3.triggers.Add(new EventTrigger.Entry { eventID = EventTriggerType.PointerExit });
        trigger3.triggers[1].callback.AddListener((data) => { OnRouteExit(b_Option); });

        EventTrigger trigger4 = b_Credit.gameObject.AddComponent<EventTrigger>();
        trigger4.triggers.Add(new EventTrigger.Entry { eventID = EventTriggerType.PointerEnter });
        trigger4.triggers[0].callback.AddListener((data) => { OnRouteEnter(b_Credit); });
        trigger4.triggers.Add(new EventTrigger.Entry { eventID = EventTriggerType.PointerExit });
        trigger4.triggers[1].callback.AddListener((data) => { OnRouteExit(b_Credit); });

        EventTrigger trigger5 = b_Quit.gameObject.AddComponent<EventTrigger>();
        trigger5.triggers.Add(new EventTrigger.Entry { eventID = EventTriggerType.PointerEnter });
        trigger5.triggers[0].callback.AddListener((data) => { OnRouteEnter(b_Quit); });
        trigger5.triggers.Add(new EventTrigger.Entry { eventID = EventTriggerType.PointerExit });
        trigger5.triggers[1].callback.AddListener((data) => { OnRouteExit(b_Quit); });
          }

    void OnRouteEnter(Button button)
    {
        int i = -1;
        if (button == b_Start) i = 0;
        if (button == b_Continue) i = 1;
        if (button == b_Option) i = 2;
        if (button == b_Credit) i = 3;
        if (button == b_Quit) i = 4;
        

        b_Image[i].SetActive(true);
    }

    void OnRouteExit(Button button)
    {
        int i = -1;
        if (button == b_Start) i = 0;
        if (button == b_Continue) i = 1;
        if (button == b_Option) i = 2;
        if (button == b_Credit) i = 3;
        if (button == b_Quit) i = 4;
      

        b_Image[i].SetActive(false) ;
    }

}
