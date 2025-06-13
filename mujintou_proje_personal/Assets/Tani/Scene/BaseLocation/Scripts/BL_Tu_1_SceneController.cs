using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.SceneManagement;

public class BL_Tu_1_SceneController : MonoBehaviour
{
    [SerializeField]
    TextControl textControl;

    List<TextAsset> textAssets;
    [SerializeField]
    GameObject tips;
    #region 山品変更

    // 追加：LocalizedTextAssetLoader
    [SerializeField] LocalizedTextAssetLoader localizedTextAssetLoader;
    #endregion

    private void Start()
    {
        textControl = GameObject.FindAnyObjectByType<TextControl>().GetComponent<TextControl>();
        Debug.Log("OPTextControl Start called");
        textControl.ResetTextData();
        textControl.ClickEventAfterTextsEnd.RemoveAllListeners();
        #region 山品変更
        localizedTextAssetLoader = GameObject.FindAnyObjectByType<LocalizedTextAssetLoader>();
        LoadLocalizedTextAssets();
        #endregion
        AddTextDataToTextControl(0);
        textControl.ClickEventAfterTextsEnd.AddListener(() =>
        {
            textControl.ClickEventAfterTextsEnd.RemoveAllListeners();
            Destroy(gameObject.transform.GetChild(0).gameObject);
            Instantiate(tips);

        });
    }


    #region 山品変更

    private void LoadLocalizedTextAssets()
    {
        textAssets = localizedTextAssetLoader.LoadTextAssetsForCurrentLocale();
        #endregion

        AddTextDataToTextControl(0);
    }
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

