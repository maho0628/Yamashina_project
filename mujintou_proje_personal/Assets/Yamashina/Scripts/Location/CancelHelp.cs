using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CancelHelp : MonoBehaviour
{
    public Button Help;
    // Start is called before the first frame update
    void Start()
    {
        Help.interactable = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerInfo.Instance.Inventry.GetVisibility())
        {
            Help.interactable = false;
        }
        else if (!PlayerInfo.Instance.Inventry.GetVisibility())
                {
            Help.interactable = true;
        }
    }
}
