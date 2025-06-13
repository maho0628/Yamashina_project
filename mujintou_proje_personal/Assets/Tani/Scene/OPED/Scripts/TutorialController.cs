using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.SceneManagement;

public class TutorialController : MonoBehaviour
{
    [SerializeField]
    TextControl textControl;
   
    List<TextAsset> textAssets;
    [SerializeField]
    SceneObject NextScene;
    #region　山品変更
    // 追加：LocalizedTextAssetLoader
    [SerializeField] LocalizedTextAssetLoader localizedTextAssetLoader;
    #endregion
    private void Start()
    {
        Debug.Log("OPTextControl Start called");
        //柴田追加分//
        var fade = GetComponent<Fading>();
        fade.Fade(Fading.type.FadeIn);
        anotherBGMPlayer ABP = GameObject.FindAnyObjectByType(typeof(anotherBGMPlayer)).GetComponent<anotherBGMPlayer>();
        StartCoroutine(ABP.FadeInAudio(3, 4));
        //ここまで//
        textControl.ResetTextData();
        textControl.ClickEventAfterTextsEnd.RemoveAllListeners();
        #region 山品変更

        LoadLocalizedTextAssets();
        AddTextDataToTextControl(0);
        #endregion



        textControl.ClickEventAfterTextsEnd.AddListener(() =>
        {
            Debug.Log("ClickEventAfterTextsEnd listener added");

            textControl.ClickEventAfterTextsEnd.RemoveAllListeners();
           
            var fade =GetComponent<Fading>();
            print(fade);
            fade.fading_time = 1.5f;
            fade.Fade(Fading.type.FadeOut);
            fade.OnFadeEnd.AddListener(()=> SceneManager.LoadScene(NextScene));
        });
    }



    #region 山品変更

    private void LoadLocalizedTextAssets()
    {
        
            textAssets = localizedTextAssetLoader.LoadTextAssetsForCurrentLocale();
            AddTextDataToTextControl(0);
        
    }
    #endregion


    void AddTextDataToTextControl(int index)
    {
        if (index > textAssets.Count)
        {
            Debug.LogError("index out of Range int textAssets");
            return;
        }

        textControl.ResetTextData();
        textControl.EndEvent.RemoveAllListeners();
        textControl.ClickEventAfterTextsEnd.RemoveAllListeners();
        #region 山品変更

        string rawData = textAssets[index].text;
        string[] splitedText = rawData.Split(char.Parse("\n"));
        foreach (var text in splitedText)
        {
            if (text == "") continue;
            textControl.AddTextData(text.Replace("**", "\n"));
        }
        #endregion

    }
}
