using UnityEngine;
using UnityEngine.Localization.Settings;
using System.Collections.Generic;

public class LocalizedTextAssetLoader : MonoBehaviour
{
    public string japaneseFolderPath = "JapaneseTextAssets"; // Resources フォルダ内のパス
    public string englishFolderPath = "EnglishTextAssets";
    public string targetFolderName = "TargetFolder"; // サブフォルダ名

    private List<TextAsset> textAssets = new List<TextAsset>();

    private void Start()
    {
        // 必要に応じて自動でロードしたい場合はここで呼び出してください
        // LoadTextAssetsForCurrentLocale();
    }

    public List<TextAsset> LoadTextAssetsForCurrentLocale()
    {
        textAssets.Clear();

        string currentLocale = LocalizationSettings.SelectedLocale.Identifier.Code;
        Debug.Log("Current Locale: " + currentLocale);

        if (currentLocale == "ja")
        {
            LoadTextAssets(japaneseFolderPath);
        }
        else if (currentLocale == "en")
        {
            LoadTextAssets(englishFolderPath);
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
        string resourcePath = System.IO.Path.Combine(relativeFolderPath, targetFolderName).Replace("\\", "/");
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
