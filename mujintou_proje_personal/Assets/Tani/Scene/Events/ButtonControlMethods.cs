using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonControlMethods : MonoBehaviour
{
    public void IsHealthUpperOf(int border,ref Button button)
    {
        button.interactable =  PlayerInfo.Instance.Health >= border;
    }
}
