using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;

public class SettingsManager : MonoBehaviour
{
    public static SettingsManager Instance; // シングルトンパターンのインスタンス
    public string currentLanguage = "ja"; // デフォルトの言語設定

    async void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // シーンをまたいでオブジェクトを保持する
        }
        else
        {
            Destroy(gameObject); // 既存のインスタンスがある場合は新しいオブジェクトを破棄する
        }

        // 設定をロードする
        LoadSettings();

        // 言語設定の初期化
        string currentLocale = SettingsManager.Instance.GetLanguage();
        Debug.Log("Current Locale: " + currentLocale);

        // LocalizationSettingsの初期化が完了するまで待機
        await LocalizationSettings.InitializationOperation.Task;

        // 言語設定の適用
        SetLocale(currentLocale);
    }

    private void SetLocale(string locale)
    {

        Locale newLocale = Locale.CreateLocale(locale);
        LocalizationSettings.SelectedLocale = newLocale;
        Debug.Log("Locale set to: " + LocalizationSettings.SelectedLocale);


    }
    public void SetLanguage(string language)
    {
        currentLanguage = language;
        SaveSettings(); // 設定を変更したときに保存する
    }

    public string GetLanguage()
    {
        return currentLanguage;
    }

    // 設定を保存するメソッド
    public void SaveSettings()
    {
        PlayerPrefs.SetString("Language", currentLanguage);
        PlayerPrefs.Save();
    }

    // 設定をロードするメソッド
    public void LoadSettings()
    {
        currentLanguage = PlayerPrefs.GetString("Language", "ja"); // デフォルトは"en"
    }
}
