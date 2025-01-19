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
        SettingsManager.Instance.SetLanguage(locale); // �ݒ��ύX���ĕۑ�
        var _ = ChangeSelectedLocale(locale); // ����ݒ��ύX
        Debug.Log(LocalizationSettings.SelectedLocale);

    }

    private async Task ChangeSelectedLocale(string locale)
    {
            Locale newLocale = Locale.CreateLocale(locale);
            Debug.Log("Changing locale to: " + newLocale);

            LocalizationSettings.SelectedLocale = newLocale;

            // LocalizationSettings�̏�����������ҋ@
            await LocalizationSettings.InitializationOperation.Task;

            Debug.Log("Locale changed to: " + LocalizationSettings.SelectedLocale);
        
    }

}
