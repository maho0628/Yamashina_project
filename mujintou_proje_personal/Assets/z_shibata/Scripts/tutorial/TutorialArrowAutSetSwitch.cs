using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialArrowAutSetSwitch : MonoBehaviour
{
    [SerializeField] GameObject arrow;
    GameObject inv;
    bool first = false;
    private void Start()
    {
        inv = PlayerInfo.Instance.Inventry.gameObject;
    }
    void Update()
    {
        if(arrow.active == true)
        {
            first = true;
        }
        if (first)
        {
            if (inv.active == false)
            {
                arrow.gameObject.SetActive(true);
            }

            if (inv.active == true)
            {
                arrow.gameObject.SetActive(false);
            }
        }
    }
}
