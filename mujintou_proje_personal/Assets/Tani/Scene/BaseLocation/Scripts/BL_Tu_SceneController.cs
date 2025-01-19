using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Localization.Settings;

public class BL_Tu_SceneController : MonoBehaviour
{
    [SerializeField]
    TextControl textControl;
    List<TextAsset> textAssets;
    [SerializeField]
    GameObject[] tips;
    [SerializeField] LocalizedTextAssetLoader localizedTextAssetLoader;


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
            Instantiate(tips[0]);

        });
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

    public void LastText()
    {
        gameObject.transform.GetChild(0).gameObject.SetActive(true);
        textControl.ResetTextData();
        textControl.ClickEventAfterTextsEnd.RemoveAllListeners();
        AddTextDataToTextControl(1);
        textControl.ClickEventAfterTextsEnd.AddListener(() =>
        {
            textControl.ClickEventAfterTextsEnd.RemoveAllListeners();
            gameObject.transform.GetChild(0).gameObject.SetActive(false);
            Instantiate(tips[1]);

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
