using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadScene_afterTitle : MonoBehaviour
{
    [SerializeField] SceneObject Title;
    [SerializeField] SwitchActivateSelf switchActivate;
    [SerializeField] Audiovolume audiovolume;

    private void Start()
    {
        //audiovolume = GameObject.FindAnyObjectByType<Audiovolume>().GetComponent<Audiovolume>();

        switchActivate = GameObject.FindAnyObjectByType<SwitchActivateSelf>().GetComponent<SwitchActivateSelf>();
    }
    public void Load()
    {
        switchActivate.SwitchActiveSelf();
        PlayerInfo.Instance.DestroySelf();
        SceneManager.LoadScene(Title);
        //audiovolume.BGM = GameObject.FindWithTag("BGM").GetComponent<AudioSource>().volume;
      
        //audiovolume.SE = GameObject.FindWithTag("SE").GetComponent<AudioSource>().volume;
        //Debug.Log(audiovolume.BGM);
        //Debug.Log(audiovolume.SE);  
        //SceneManager.sceneLoaded += OnSceneLoaded;
    }

    //    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    //    {
    //        PlayerInfo.Instance.SwitchUIVisibility(); ;
    //    }
}

