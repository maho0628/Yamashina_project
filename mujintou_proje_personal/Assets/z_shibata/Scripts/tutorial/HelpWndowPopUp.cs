using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelpWndowPopUp : MonoBehaviour
{
    [SerializeField] GameObject prefab;
    public void HelpWindow()
    {
        Instantiate(prefab);

    }
}
