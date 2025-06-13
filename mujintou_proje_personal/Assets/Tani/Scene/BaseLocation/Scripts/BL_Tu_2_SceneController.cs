using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;
using UnityEngine.Localization.Settings;
using System.Threading.Tasks;

public class BL_Tu_2_SceneController : MonoBehaviour
{
    [SerializeField]
    TextControl textControl;
    List<TextAsset> textAssets;
    [SerializeField]
    GameObject tips;
    [SerializeField]
    GameObject nextTips;
    [SerializeField]
    SceneObject nextScene;
    #region 山品変更
    [SerializeField] LocalizedTextAssetLoader localizedTextAssetLoader;
    #endregion
    List<SlotManager> slots = new List<SlotManager>();
    private void Start()
    {
        textControl.ResetTextData();
        textControl.ClickEventAfterTextsEnd.RemoveAllListeners();
        #region 山品変更

        localizedTextAssetLoader = GameObject.FindAnyObjectByType<LocalizedTextAssetLoader>();
        LoadLocalizedTextAssets();
        AddTextDataToTextControl(0);
        #endregion

        textControl.ClickEventAfterTextsEnd.AddListener(() =>
        {
            textControl.ClickEventAfterTextsEnd.RemoveAllListeners();
            gameObject.transform.GetChild(0).gameObject.SetActive(false);
            Instantiate(tips);

        });

        PlayerInfo.Instance.OnMaxActionValueChange.AddListener(MakeTips2);
        PlayerInfo.Instance.Inventry.SetVisible(true);
        PlayerInfo.Instance.gameObject.GetComponentInChildren<DetailPanel>().OnItemUse += OnCoconutsUsed;
        PlayerInfo.Instance.Inventry.SetVisible(false);


    }
    #region 山品変更

    private void LoadLocalizedTextAssets()
    {
        textAssets = localizedTextAssetLoader.LoadTextAssetsForCurrentLocale();
        AddTextDataToTextControl(0);
    }
    #endregion

    void OnCoconutsUsed(Items.Item_ID iD)
    {
        if (iD == Items.Item_ID.item_craft_coconutJuice)
        {
            PlayerInfo.Instance.Inventry.SetVisible(true);
            PlayerInfo.Instance.gameObject.GetComponentInChildren<DetailPanel>().OnItemUse -= OnCoconutsUsed;
            PlayerInfo.Instance.Inventry.SetVisible(false);
            PlayerInfo.Instance.MaxActionValue++;
        }
    }

    void MakeTips2()
    {
        if (PlayerInfo.Instance.MaxActionValue == 8)
        {
            return;
        }
        if (PlayerInfo.Instance.MaxActionValue == 6 || PlayerInfo.Instance.MaxActionValue == 9)
        {
            PlayerInfo.Instance.OnMaxActionValueChange.RemoveListener(MakeTips2);
            Instantiate(nextTips);
            PlayerInfo.Instance.Inventry.SetVisible(false);
        }
    }

    public void NextText()
    {
        gameObject.transform.GetChild(0).gameObject.SetActive(true);
        textControl.ResetTextData();
        textControl.ResetTextData();
        textControl.ClickEventAfterTextsEnd.RemoveAllListeners();
        AddTextDataToTextControl(1);
        textControl.ClickEventAfterTextsEnd.AddListener(() =>
        {
            textControl.ClickEventAfterTextsEnd.RemoveAllListeners();
            gameObject.transform.GetChild(0).gameObject.SetActive(false);
            var fading = (Fading)GameObject.FindAnyObjectByType(typeof(Fading));
            fading.Fade(Fading.type.FadeOut);
            fading.OnFadeEnd.AddListener(() =>
            {
                SceneManager.LoadScene(nextScene);
            });

        });
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