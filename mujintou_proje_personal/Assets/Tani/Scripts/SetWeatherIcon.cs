using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetWeatherIcon : MonoBehaviour
{
    [SerializeField]
    ImageViewWithSprites view;

    private void Start()
    {
        view.SetImageSprite((int)PlayerInfo.Instance.weather);
    }
}
