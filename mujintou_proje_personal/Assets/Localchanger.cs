using Opening;
using System.Threading.Tasks;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using UnityEngine.UI;

public class Localchanger : MonoBehaviour
{


    public void Change(string locale)
    {
        SettingsManager.Instance.SetLanguage(locale); // 設定を変更して保存
        var _ = ChangeSelectedLocale(locale); // 言語設定を変更
        Debug.Log(LocalizationSettings.SelectedLocale);

    }

    private async Task ChangeSelectedLocale(string locale)
    {
            Locale newLocale = Locale.CreateLocale(locale);
            Debug.Log("Changing locale to: " + newLocale);

            LocalizationSettings.SelectedLocale = newLocale;

            // LocalizationSettingsの初期化完了を待機
            await LocalizationSettings.InitializationOperation.Task;

            Debug.Log("Locale changed to: " + LocalizationSettings.SelectedLocale);
        
    }

}
