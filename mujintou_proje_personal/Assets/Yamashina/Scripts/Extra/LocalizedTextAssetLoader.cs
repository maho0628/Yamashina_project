using UnityEngine;
using UnityEngine.Localization.Settings;
using System.Collections.Generic;

/// <summary>
/// ローカライズ対応したテキストアセットを読み込むためのクラス
/// </summary>
public class LocalizedTextAssetLoader : MonoBehaviour
{
    /// <summary>
    /// 日本語用テキストが入っているフォルダ名
    /// </summary>
    [SerializeField, Header("日本語ローカライズテキストフォルダ")]
    private string japaneseLocalizationFolder = "JapaneseTextAssets";

    /// <summary>
    /// 英語ローカライズテキストフォルダ
    /// </summary>
    [SerializeField, Header("英語ローカライズテキストフォルダ")]
    private string englishLocalizationFolder = "EnglishTextAssets";

    /// <summary>
    /// ローカライズ対象サブフォルダ
    /// </summary>
    [SerializeField, Header("ローカライズ対象サブフォルダ")]
    private string localizationSubFolder = "TargetFolder";

    /// <summary>
    /// フォルダ内のテキストをすべて読み込むためのリスト
    /// </summary>
    private List<TextAsset> textAssets = new List<TextAsset>();

    /// <summary>
    /// 日本語用テキストが入っているフォルダ名の読み取り専用
    /// </summary>
    public string JapaneseLocalizationFolder
    {
        get { return japaneseLocalizationFolder; }
    }
    //英語ローカライズテキストフォルダの読み取り専用
    public string EnglishLocalizationFolder
    {
        get { return englishLocalizationFolder; }
    }

    /// <summary>
    /// 対応した言語のテキストアセットを返すための関数
    /// </summary>
    /// <returns> List<TextAsset></returns>
    public List<TextAsset> LoadTextAssetsForCurrentLocale()
    {
        //List内のテキストアセットを全て削除して空に
        textAssets.Clear();

        //現在選択している言語のコードを取得
        string currentLocale = LocalizationSettings.SelectedLocale.Identifier.Code;
        Debug.Log("Current Locale: " + currentLocale);

        //日本語なら
        if (currentLocale == "ja")
        {
          
            LoadTextAssets(japaneseLocalizationFolder);
        }
        //英語なら
        else if (currentLocale == "en")
        {
            LoadTextAssets(englishLocalizationFolder);
        }
        //それ以外
        else
        {
            Debug.LogWarning("対応していないロケール: " + currentLocale);
        }

        return textAssets;
    }

    /// <summary>
    /// 対応したパスのテキストアセットを読み込むための関数
    /// </summary>
    /// <param name="relativeFolderPath"></param>
    public void LoadTextAssets(string relativeFolderPath)
    {
        // Resources.LoadAll で使うパス形式に変換（スラッシュ統一）
        string resourcePath = System.IO.Path.Combine(relativeFolderPath, localizationSubFolder).Replace("\\", "/");
        Debug.Log("Loading Resources from: " + resourcePath);

        //リソースフォルダから対応したフォルダのテキストアセットを読み込む
        TextAsset[] loadedAssets = Resources.LoadAll<TextAsset>(resourcePath);

        //何も入ってないのでエラーをだして終了
        if (loadedAssets.Length == 0)
        {
            Debug.LogError($"TextAssets not found at Resources/{resourcePath}");
            return; 
        }

        //テキストアセットリストに追加
        textAssets.AddRange(loadedAssets);

        //現在入っているテキストアセットをデバッグログで表示
        foreach (var asset in loadedAssets)
        {
            Debug.Log($"Loaded TextAsset: {asset.name}");
        }
    }
}
