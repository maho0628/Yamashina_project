using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchActivateSelf : MonoBehaviour
{
    public void SwitchActiveSelf()
    {
        gameObject.SetActive(!gameObject.activeSelf);
    }
}
