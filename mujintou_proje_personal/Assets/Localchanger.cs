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
        SettingsManager.Instance.SetLanguage(locale); // İ’è‚ğ•ÏX‚µ‚Ä•Û‘¶
        var _ = ChangeSelectedLocale(locale); // Œ¾Œêİ’è‚ğ•ÏX
        Debug.Log(LocalizationSettings.SelectedLocale);

    }

    private async Task ChangeSelectedLocale(string locale)
    {
            Locale newLocale = Locale.CreateLocale(locale);
            Debug.Log("Changing locale to: " + newLocale);

            LocalizationSettings.SelectedLocale = newLocale;

            // LocalizationSettings‚Ì‰Šú‰»Š®—¹‚ğ‘Ò‹@
            await LocalizationSettings.InitializationOperation.Task;

            Debug.Log("Locale changed to: " + LocalizationSettings.SelectedLocale);
        
    }

}
