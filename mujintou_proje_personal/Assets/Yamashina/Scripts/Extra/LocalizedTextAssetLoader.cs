using UnityEngine;
using UnityEngine.Localization.Settings;
using System.Collections.Generic;

public class LocalizedTextAssetLoader : MonoBehaviour
{
    /// <summary>
    /// 日本語用テキストが入っているフォルダ名
    /// </summary>
    [SerializeField, Header("日本語ローカライズテキストフォルダ")]
    private string japaneseLocalizationFolder = "JapaneseTextAssets";

    [SerializeField, Header("英語ローカライズテキストフォルダ")]
    private string englishLocalizationFolder = "EnglishTextAssets";

    [SerializeField, Header("ローカライズ対象サブフォルダ")]
    private string localizationSubFolder = "TargetFolder";

    private List<TextAsset> textAssets = new List<TextAsset>();

    /// <summary>
    /// 
    /// </summary>
    public  string JapaneseLocalizationFolder
    {
        get { return japaneseLocalizationFolder; }  
    }
    public string EnglishLocalizationFolder
    {
        get { return englishLocalizationFolder; }   
    }

    public List<TextAsset> LoadTextAssetsForCurrentLocale()
    {
        textAssets.Clear();

        string currentLocale = LocalizationSettings.SelectedLocale.Identifier.Code;
        Debug.Log("Current Locale: " + currentLocale);

        if (currentLocale == "ja")
        {
            LoadTextAssets(japaneseLocalizationFolder);
        }
        else if (currentLocale == "en")
        {
            LoadTextAssets(englishLocalizationFolder);
        }
        else
        {
            Debug.LogWarning("対応していないロケール: " + currentLocale);
        }

        return textAssets;
    }

    public void LoadTextAssets(string relativeFolderPath)
    {
        // Resources.LoadAll で使うパス形式に変換（スラッシュ統一）
        string resourcePath = System.IO.Path.Combine(relativeFolderPath, localizationSubFolder).Replace("\\", "/");
        Debug.Log("Loading Resources from: " + resourcePath);

        TextAsset[] loadedAssets = Resources.LoadAll<TextAsset>(resourcePath);

        if (loadedAssets.Length == 0)
        {
            Debug.LogError($"TextAssets not found at Resources/{resourcePath}");
        }

        textAssets.AddRange(loadedAssets);

        foreach (var asset in loadedAssets)
        {
            Debug.Log($"Loaded TextAsset: {asset.name}");
        }
    }
}
