using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class UpperBorder : MonoBehaviour
{
    [SerializeField]
    int upper_border = 50;

    private void Start()
    {
        GetComponent<Button>().interactable = PlayerInfo.Instance.Health >= upper_border; 
    }
}
