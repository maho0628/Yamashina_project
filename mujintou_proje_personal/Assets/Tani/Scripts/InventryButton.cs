using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventryButton : MonoBehaviour
{

    void Start()
    {
        GetComponent<Button>().onClick.AddListener(() =>
        {
            PlayerInfo.Instance.Inventry.SwitchVisible();
            PlayerInfo.Instance.OnVisibilityChanged();
        });
    }


    void Update()
    {
        
    }
}
