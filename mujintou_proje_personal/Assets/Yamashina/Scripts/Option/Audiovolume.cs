using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;


public class Audiovolume : MonoBehaviour
{
    public static Audiovolume instance;
    public float BGM ;
    public float SE;
    public AudioSource audioSourceBGM;//BGM

    public AudioSource audioSourceSE;//SoundEffect
    private void Awake()
    {

        Debug.Log(BGM);
        Debug.Log(SE);
        GameObject.FindWithTag("BGM").GetComponent<AudioSource>().volume = BGM;
        GameObject.FindWithTag("BGM").GetComponent<AudioSource>().volume = SE;

        audioSourceBGM = GameObject.FindWithTag("BGM").GetComponent<AudioSource>();
        audioSourceSE = GameObject.FindWithTag("SE").GetComponent<AudioSource>();

       
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
       
    }


    


    public void SetBgmVolume(float bgmVolume)
    {
        audioSourceBGM.volume = bgmVolume;

    }

    public void SetSeVolume(float seVolume)
    {
        audioSourceSE.volume = seVolume;

    }
   
       


}

