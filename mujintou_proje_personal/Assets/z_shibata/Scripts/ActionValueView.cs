using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionValueView : MonoBehaviour
{
    [SerializeField] Text text1;
    [SerializeField] Text text2;
    private void OnEnable()
    {
        text1.text = PlayerInfo.Instance.ActionValue.ToString();
        text2.text = PlayerInfo.Instance.MaxActionValue.ToString();
    }
}
