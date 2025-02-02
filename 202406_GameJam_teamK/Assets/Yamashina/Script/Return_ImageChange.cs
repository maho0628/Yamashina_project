using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer.Internal;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Return_ImageChange : MonoBehaviour
{
    [SerializeField] GameObject hightlightImage;
    [SerializeField] Sprite hightLight;
    [SerializeField] Sprite hightLightOff;


    [SerializeField] Button b_Start;
    [SerializeField] EventTrigger butttoEffect;

    // Start is called before the first frame update
    private void Start()
    {
        butttoEffect.triggers.Add(new EventTrigger.Entry { eventID = EventTriggerType.PointerEnter });
        butttoEffect.triggers[0].callback.AddListener((data) => { OnRouteEnter(b_Start); });
        butttoEffect.triggers.Add(new EventTrigger.Entry { eventID = EventTriggerType.PointerExit });
        butttoEffect.triggers[1].callback.AddListener((data) => { OnRouteExit(b_Start); });

    }
    void OnRouteEnter(Button button)
    {
       
        //b_Image[i].SetActive(true);
        hightlightImage.GetComponent<Image>().sprite = hightLight;
    }

    void OnRouteExit(Button button)
    {
       

        //b_Image[i].SetActive(false);
        hightlightImage.GetComponent<Image>().sprite = hightLightOff;
    }
}
