using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialArrow : MonoBehaviour
{
    public void ArrowSet()
    {
        GameObject arrow = GameObject.Find("ArrowCanvas");
        arrow.transform.GetChild(0).gameObject.SetActive(true);
    }
    public void ArrowFalse()
    {
        GameObject arrow = GameObject.Find("ArrowCanvas");
        arrow.transform.GetChild(0).gameObject.SetActive(false);
    }
}
