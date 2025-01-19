using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonToNormal0 : MonoBehaviour
{
    private void Start()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(() =>
        { ((EventPanelBase)GameObject.FindAnyObjectByType(typeof(EventPanelBase))).ChoiseSelected(0); });   
    }
}
