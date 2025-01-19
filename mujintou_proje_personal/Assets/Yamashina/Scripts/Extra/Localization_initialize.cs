using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using UnityEngine.Localization.Tables;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.ResourceManagement.AsyncOperations;

public class Localization_initialize : MonoBehaviour
{



    public Text uiText;  // UIのテキストコンポーネント
    private string tableName = "Local_Ja_toEn";  // ローカライズテーブルの名前
    void Start()
    {

        // GameObjectの名前をエントリーキーとして使用
        string entryKey = gameObject.name;

        // ローカライズされたテキストを非同期で取得
        StartCoroutine(InitializeLocalization(entryKey));
    }

   public IEnumerator InitializeLocalization(string entryKey)
    {
      // LocalizationSettingsが初期化されるまで待機
        yield return LocalizationSettings.InitializationOperation;

        // ローカライズされた文字列を非同期で取得
        var localizedString = LocalizationSettings.StringDatabase.GetLocalizedStringAsync(tableName, entryKey);
        yield return localizedString;

        // テキストを設定
        if (localizedString.Status == AsyncOperationStatus.Succeeded)
        {
            uiText.text = localizedString.Result;  // 成功した場合、テキストを設定
        }
        else
        {
            Debug.LogError($"Localization failed for key: {entryKey} in table: {tableName}");
            uiText.text = "";  // エラーの場合、テキストを空に設定
        }
    }
}

       

    







