using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class optionButtonUnsetInventry : MonoBehaviour
{
    public void InventryUnset()
    {
        PlayerInfo.Instance.Inventry.SetVisible(false);
    }
}
