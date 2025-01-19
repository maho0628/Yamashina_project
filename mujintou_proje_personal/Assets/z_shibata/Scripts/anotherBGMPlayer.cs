using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.WebSockets;
using UnityEngine;
using UnityEngine.SceneManagement;

public class anotherBGMPlayer : MonoBehaviour
{
    [SerializeField] AudioClip[] BGMclips;
    [SerializeField] int num;
    [SerializeField] SceneObject baseLocation;
    private void Start()
    {

        Invoke("PlayBGMofDiray", 0.1f);

    }
    public void ChooseSongs_BGM(int num)
    {
            GameObject.FindWithTag("BGM").GetComponent<AudioSource>().clip = BGMclips[num];
            GameObject.FindWithTag("BGM").GetComponent<AudioSource>().Play();
    }
    public void PlayBGMofDiray()
    {
        //山品追加分
        if (PlayerInfo.InstanceNullable == null) return;

        Audiovolume.instance.audioSourceBGM = GameObject.FindWithTag("BGM").GetComponent<AudioSource>();
        GameObject.FindWithTag("BGM").GetComponent<AudioSource>().volume = Audiovolume.instance.BGM;
        //こっから柴田都合
        if(baseLocation != null)
        { 
            if (baseLocation == SceneManager.GetActiveScene().name)
            {
                num = 2;
                if(PlayerInfo.Instance.Fire >= 1.0f)
                {
                    num = 1;
                }
            }
        }
        GameObject.FindWithTag("BGM").GetComponent<AudioSource>().clip = BGMclips[num];
        GameObject.FindWithTag("BGM").GetComponent<AudioSource>().Play();
    }
    // フェードアウトの関数
    public IEnumerator FadeOutAudio(float fadeDuration)
    {
        AudioSource audioSource = GameObject.FindWithTag("BGM").GetComponent<AudioSource>();
        float startVolume = audioSource.volume;

        while (audioSource.volume > 0)
        {
            audioSource.volume -= startVolume * Time.deltaTime / fadeDuration;
            yield return null;
        }

        audioSource.Stop();
        audioSource.volume = startVolume;
    }

    // フェードインの関数
    public IEnumerator FadeInAudio(int num, float fadeDuration)
    {
        AudioSource audioSource = GameObject.FindWithTag("BGM").GetComponent<AudioSource>();
        float setVolume = audioSource.volume;

        audioSource.volume = 0;
        audioSource.clip = BGMclips[num];
        audioSource.Play();

        while (audioSource.volume < setVolume)
        {
            audioSource.volume += Time.deltaTime / fadeDuration;
            yield return null;
        }
    }
    public void ChangeBGM(int bgmNum)
    {
        float fadeDuration = 1;
        StartCoroutine(ChangeBGMCoroutine(bgmNum, fadeDuration));
    }

    private IEnumerator ChangeBGMCoroutine(int bgmNum_, float fadeDuration)
    {
        AudioSource audio = GameObject.FindWithTag("BGM").GetComponent<AudioSource>();
        if (audio.isPlaying)
        {
            yield return StartCoroutine(FadeOutAudio(fadeDuration));
        }

        
        yield return StartCoroutine(FadeInAudio(bgmNum_, fadeDuration));
    }
}