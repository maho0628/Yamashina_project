using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainSoundPlayer : MonoBehaviour
{
    void Start()
    {
        GameObject.FindWithTag("BGM").GetComponent<AudioSource>().volume = Audiovolume.instance.BGM;

        GetComponent<AudioSource>().volume = GameObject.FindGameObjectWithTag("BGM").GetComponent<AudioSource>().volume;

        if(PlayerInfo.Instance.weather == PlayerInfo.Weather.Rainy)
        {
            GetComponent<AudioSource>().Play();
        }
    }

}
