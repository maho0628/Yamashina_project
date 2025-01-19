using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;
using UnityEngine.EventSystems;

public class BadEndController : MonoBehaviour
{
    [SerializeField] Image backImage_Black;
    [SerializeField] GameObject textBox;
    [SerializeField] Fading fade;
    [SerializeField] GameObject SEAudio;
    //[SerializeField] GameObject destroyObject;
    [SerializeField] SceneObject title;

    public void Bad()
    {
        StartCoroutine(BadEnd());
    }
    IEnumerator BadEnd()
    {
        Debug.Log(PlayerInfo.Instance.Health);
        Debug.Log(PlayerInfo.Instance.Day.day);

        //var player = GameObject.FindAnyObjectByType<PlayerInfo>();
        //GameObject playerOb = player.gameObject;
        ////playerOb.transform.GetChild(0).transform.GetChild(0).transform.GetChild(0).transform.GetChild(0).gameObject.SetActive(false);
        fade.fading_time = 3f;
        fade.Fade(Fading.type.FadeOut);
        fade.OnFadeEnd.AddListener(() =>
        { 

            textBox.gameObject.SetActive(false);
            backImage_Black.gameObject.SetActive(false);
            if(GameObject.FindGameObjectWithTag("SE")!=null)
            {
                SEAudio.GetComponent<anotherSoundPlayer>().ChooseSongs_SE(10);
            }
            fade.Fade(Fading.type.FadeIn);


        });

        yield return null;


    }

    public void BackToTitle()
    {
        SEAudio.GetComponent<anotherSoundPlayer>().ChooseSongs_SE(0);

        var loaded = SceneManager.LoadSceneAsync(title);
        loaded.allowSceneActivation = false;
        fade.fading_time = 3f;

        fade.Fade(Fading.type.FadeOut);
        fade.OnFadeEnd.AddListener(() =>
        {
            fade.Fade(Fading.type.FadeIn);
        });

        //プレイヤーの破壊をここに移動//
        //    if (PlayerInfo.InstanceNullable)
        //{
        //    PlayerInfo.Instance.DestroySelf();
        //}
        //DataManager.ErasePlayerSaveData();

        //プレイヤーの破壊をここに移動//
        loaded.allowSceneActivation = true;



    }
}