using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class anotherSoundPlayer : MonoBehaviour
{
    int Option_Count;

    private void Start()
    {
        Option_Count = 0;
    }

    public AudioClip[] audioClipSE;
    public void ChooseSongs_SE(int num)
    {
        GameObject au = GameObject.FindWithTag("SE");

        if (au.GetComponent<SoundCoolTime>().canPlay)
        {
            au.GetComponent<SoundCoolTime>().canPlay = false;
            AudioVolume.instance.audioSourceSE = GameObject.FindWithTag("SE").GetComponent<AudioSource>();

        //if (!AudioVolume.Instance.audioSourceSE.isPlaying)
        //{
            GameObject.FindWithTag("SE").GetComponent<AudioSource>().PlayOneShot(audioClipSE[num]);
        //}

            GameObject.FindWithTag("SE").GetComponent<AudioSource>().volume = AudioVolume.instance.SE;
        
        }
       
    }

    public void ChooseSongs_SE_Opsion()
    {
        Option_Count++;

        if (Option_Count % 2 == 0) 
        {
            ChooseSongs_SE(1);          
        }
        else
        {
            ChooseSongs_SE(2);
        }
    }
}
