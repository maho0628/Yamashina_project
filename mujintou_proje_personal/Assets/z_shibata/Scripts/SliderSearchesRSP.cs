using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderSearchesRSP : MonoBehaviour
{
    public void RainChanged()
    {
        GameObject RSP = GameObject.Find("RainSoundPlayer");
        if (RSP != null)
        {
            RSP.GetComponent<AudioSource>().volume = GetComponent<Slider>().value;
        }
    }
}
