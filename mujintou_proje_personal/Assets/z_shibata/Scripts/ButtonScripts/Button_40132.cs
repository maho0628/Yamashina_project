using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static PlayerInfo;

public class Button_40132 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

        gameObject.GetComponent<Button>().onClick.AddListener(() =>
        {
            PlayerInfo.Instance.weather = PlayerInfo.Weather.Cloudy;
        });

    }
}
