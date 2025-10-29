using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadScene_afterTitle : MonoBehaviour
{
    [SerializeField] SceneObject Title;
    [SerializeField] SwitchActivateSelf switchActivate;
    [SerializeField] AudioVolume AudioVolume;

    private void Start()
    {
        //AudioVolume = GameObject.FindAnyObjectByType<AudioVolume>().GetComponent<AudioVolume>();

        switchActivate = GameObject.FindAnyObjectByType<SwitchActivateSelf>().GetComponent<SwitchActivateSelf>();
    }
    public void Load()
    {
        switchActivate.SwitchActiveSelf();
        PlayerInfo.Instance.DestroySelf();
        SceneManager.LoadScene(Title);
        //AudioVolume.BGM = GameObject.FindWithTag("BGM").GetComponent<AudioSource>().volume;
      
        //AudioVolume.SE = GameObject.FindWithTag("SE").GetComponent<AudioSource>().volume;
        //Debug.Log(AudioVolume.BGM);
        //Debug.Log(AudioVolume.SE);  
        //SceneManager.sceneLoaded += OnSceneLoaded;
    }

    //    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    //    {
    //        PlayerInfo.Instance.SwitchUIVisibility(); ;
    //    }
}

