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
    [SerializeField] LocalizedTextAssetLoader localizedTextAssetLoader;

    List<SlotManager> slots = new List<SlotManager>();
    private void Start()
    {
        textControl.ResetTextData();
        textControl.ClickEventAfterTextsEnd.RemoveAllListeners();
        localizedTextAssetLoader = GameObject.FindAnyObjectByType<LocalizedTextAssetLoader>();
        LoadLocalizedTextAssets();
        AddTextDataToTextControl(0);
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
    public async void ChangeLanguage(string locale)
    {
        await ChangeSelectedLocale(locale);
        Debug.Log(locale);
    }

    private async Task ChangeSelectedLocale(string locale)
    {
        var selectedLocale = LocalizationSettings.AvailableLocales.Locales.Find(l => l.Identifier.Code == locale);
        if (selectedLocale != null)
        {
            LocalizationSettings.SelectedLocale = selectedLocale;
            await LocalizationSettings.InitializationOperation.Task;
        }
        else
        {
            Debug.LogError($"Locale '{locale}' not found.");
        }
    }
    public void ReloadLocalizedText()
    {
        Debug.Log("ReloadLocalizedText called");

        textControl.ResetTextData();
        AddTextDataToTextControl(0); // ここでテキストを再読み込みします
    }
    private void LoadLocalizedTextAssets()
    {
        textAssets = localizedTextAssetLoader.LoadTextAssetsForCurrentLocale();
        AddTextDataToTextControl(0);
    }
    void OnCoconutsUsed(Items.Item_ID iD)
    {
        if(iD == Items.Item_ID.item_craft_coconutJuice)
        {
            PlayerInfo.Instance.Inventry.SetVisible(true);
            PlayerInfo.Instance.gameObject.GetComponentInChildren<DetailPanel>().OnItemUse -= OnCoconutsUsed;
            PlayerInfo.Instance.Inventry.SetVisible(false);
            PlayerInfo.Instance.MaxActionValue++;
        }
    }

    void MakeTips2()
    {
        if(PlayerInfo.Instance.MaxActionValue == 8)
        { 
            return;
        }
        if(PlayerInfo.Instance.MaxActionValue == 6 || PlayerInfo.Instance.MaxActionValue == 9)
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

        string rawData = textAssets[index].text;
        string[] splitedText = rawData.Split(char.Parse("\n"));
        foreach (var text in splitedText)
        {
            if (text == "") continue;
            textControl.AddTextData(text.Replace("**", "\n"));
        }
    }
}