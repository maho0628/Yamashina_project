using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class multiAudio : MonoBehaviour
{



    public AudioClip[] audioClipsBGM;//BGMÇÃëfçﬁ

    public AudioClip[] audioClipSE;//Ç»ÇÁÇ∑âπåπ



   




    //É{ÉäÉÖÅ[ÉÄïœä∑
    public float ConvertVolumeToDb(float volume)
    {
        return Mathf.Clamp(Mathf.Log10(Mathf.Clamp(volume, 0f, 1f)) * 20f, -80f, 0f);
    }

    //BGMê›íË
    public void ChooseSongs_BGM(int num)
    {
        switch (num)
        {
           
            case 0:
                GameObject.FindWithTag("BGM").GetComponent<AudioSource>().clip = audioClipsBGM[num];
                audioClipsBGM = GetComponent<multiAudio>().audioClipsBGM;

                GameObject.FindWithTag("BGM").GetComponent<AudioSource>().Play();
                break;
            case 1:
                GameObject.FindWithTag("BGM").GetComponent<AudioSource>().clip = audioClipsBGM[num];
                audioClipsBGM = GetComponent<multiAudio>().audioClipsBGM;

                GameObject.FindWithTag("BGM").GetComponent<AudioSource>().Play();
                break;
        }
    }
    //SEê›íË

    public void ChooseSongs_SE(int num)
    {
        if (GameObject.FindWithTag("SE").GetComponent<SoundCoolTime>().canPlay)
        {
            switch (num)
            {
                case 0:
                    //if (!Audiovolume.instance.audioSourceSE.isPlaying)
                    //{
                    GameObject.FindWithTag("SE").GetComponent<SoundCoolTime>().canPlay = false;
                    GameObject.FindWithTag("SE").GetComponent<AudioSource>().clip = audioClipSE[num];
                    audioClipSE = GetComponent<multiAudio>().audioClipSE;
                    GameObject.FindWithTag("SE").GetComponent<AudioSource>().volume = Audiovolume.instance.SE;


                    GameObject.FindWithTag("SE").GetComponent<AudioSource>().PlayOneShot(audioClipSE[num]);
                    //}
                    break;

                case 1:
                    //if (!Audiovolume.instance.audioSourceSE.isPlaying)
                    //{
                    GameObject.FindWithTag("SE").GetComponent<SoundCoolTime>().canPlay = false;
                    GameObject.FindWithTag("SE").GetComponent<AudioSource>().clip = audioClipSE[num];
                    audioClipSE = GetComponent<multiAudio>().audioClipSE;
                    GameObject.FindWithTag("SE").GetComponent<AudioSource>().volume = Audiovolume.instance.SE;

                    GameObject.FindWithTag("SE").GetComponent<AudioSource>().PlayOneShot(audioClipSE[num]);
                    //}
                    break;
                case 2:
                    //if (!Audiovolume.instance.audioSourceSE.isPlaying)
                    //{
                    GameObject.FindWithTag("SE").GetComponent<SoundCoolTime>().canPlay = false;
                    GameObject.FindWithTag("SE").GetComponent<AudioSource>().clip = audioClipSE[num];
                    audioClipSE = GetComponent<multiAudio>().audioClipSE;
                    GameObject.FindWithTag("SE").GetComponent<AudioSource>().volume = Audiovolume.instance.SE;

                    GameObject.FindWithTag("SE").GetComponent<AudioSource>().PlayOneShot(audioClipSE[num]);
                    //}
                    break;
            }
        }
    }

}

