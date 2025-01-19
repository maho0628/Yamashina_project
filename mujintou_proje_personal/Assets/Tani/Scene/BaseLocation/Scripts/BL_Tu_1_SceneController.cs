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

    // 追加：LocalizedTextAssetLoader
    [SerializeField] LocalizedTextAssetLoader localizedTextAssetLoader;

    private void Start()
    {
        textControl=GameObject.FindAnyObjectByType<TextControl>().GetComponent<TextControl>();  
        Debug.Log("OPTextControl Start called");
        textControl.ResetTextData();
        textControl.ClickEventAfterTextsEnd.RemoveAllListeners();
        localizedTextAssetLoader=GameObject.FindAnyObjectByType<LocalizedTextAssetLoader>();    
        LoadLocalizedTextAssets();
        AddTextDataToTextControl(0);
        textControl.ClickEventAfterTextsEnd.AddListener(() =>
        {
            textControl.ClickEventAfterTextsEnd.RemoveAllListeners();
            Destroy(gameObject.transform.GetChild(0).gameObject);
            Instantiate(tips);

        });
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

