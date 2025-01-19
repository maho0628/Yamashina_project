using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EventSceneControllerBase : MonoBehaviour
{
    [SerializeField]
    EventPanelBase eventPanel;
    [SerializeField]
    SceneObject sceneToGetBack;
    [SerializeField]
    Fading fading;
    [SerializeField]
    GameObject imageInMoveing;
    [SerializeField]
    Image scene_BG;
    [SerializeField]
    List<Sprite> scene_back_ground_images;
    [SerializeField]
    SceneObject trueEnd;
    [SerializeField]
    SceneObject badEnd;
    [SerializeField]
    SceneObject volcanoEvent;
    [SerializeField]
    SceneObject caveEvent_fire;
    [SerializeField]
    SceneObject caveEvent_light;
    [SerializeField]
    SceneObject tutorialBase;

    private void Awake()
    {
        
    }
    void Start()
    {
        scene_BG.sprite = scene_back_ground_images[(int)PlayerInfo.Instance.weather];
        imageInMoveing.SetActive(true);
        StartCoroutine(HideMovingImage());
        
    }




    public void RestartEvent_NoMovingImage()
    {
        fading.Fade(Fading.type.FadeOut);
        fading.OnFadeEnd.AddListener(() =>
        {
            StartCoroutine(FadeinAfterOneFrame());

        });
    }

    IEnumerator FadeinAfterOneFrame()
    {
        yield return null;
        fading.Fade(Fading.type.FadeIn);
        eventPanel.SetRandomEvent();

    }

    public void RestartEvent()
    {
        //フェード終了時に呼ばれる
        fading.OnFadeEnd.AddListener(() =>
        {
            imageInMoveing.SetActive(true);
            StartCoroutine(HideMovingImage());

        });
        fading.Fade(Fading.type.FadeOut);
    }

    IEnumerator HideMovingImage()
    {
        yield return new WaitForSeconds(3f);
        imageInMoveing.SetActive(false);
        fading.Fade(Fading.type.FadeIn);
        eventPanel.SetRandomEvent();
    }

    public void BackHome()
    {
        fading.Fade(Fading.type.FadeOut);
        fading.OnFadeEnd.AddListener(() =>
        {
            PlayerInfo.Instance.DoAction();
            SceneManager.LoadScene(sceneToGetBack);
        });
        
    }

    public void TrueEnd()
    {
        fading.Fade(Fading.type.FadeOut);
        fading.OnFadeEnd.AddListener(() => { SceneManager.LoadScene(trueEnd); });
    }

    public void BadEnd()
    {
        fading.Fade(Fading.type.FadeOut);
        fading.OnFadeEnd.AddListener(() => { SceneManager.LoadScene(badEnd); });
    }

    public void GoVolcano()
    {

        fading.Fade(Fading.type.FadeOut);
        fading.OnFadeEnd.AddListener(() => { SceneManager.LoadScene(volcanoEvent); });
    }
    public void GoCave()
    {
        PlayerInfo.Instance.ActionValue--;

        fading.Fade(Fading.type.FadeOut);
        fading.OnFadeEnd.AddListener(() => { SceneManager.LoadScene(caveEvent_fire); });
    }
    public void GoCave_L()
    {
        PlayerInfo.Instance.ActionValue--;

        fading.Fade(Fading.type.FadeOut);
        fading.OnFadeEnd.AddListener(() => { SceneManager.LoadScene(caveEvent_light); });
    }
    public void GoTutorialBase()
    {

        fading.Fade(Fading.type.FadeOut);
        fading.OnFadeEnd.AddListener(() => { SceneManager.LoadScene(tutorialBase); });
    }
}
