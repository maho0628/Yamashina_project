using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Choice_10041 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(() =>
        {
            //var a = GetComponent<EventPanelBase>();
            //a.SelectChoise0();
        });
        
    }
}
