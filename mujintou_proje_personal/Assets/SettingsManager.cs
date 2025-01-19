using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;

public class SettingsManager : MonoBehaviour
{
    public static SettingsManager Instance; // �V���O���g���p�^�[���̃C���X�^���X
    public string currentLanguage = "ja"; // �f�t�H���g�̌���ݒ�

    async void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // �V�[�����܂����ŃI�u�W�F�N�g��ێ�����
        }
        else
        {
            Destroy(gameObject); // �����̃C���X�^���X������ꍇ�͐V�����I�u�W�F�N�g��j������
        }

        // �ݒ�����[�h����
        LoadSettings();

        // ����ݒ�̏�����
        string currentLocale = SettingsManager.Instance.GetLanguage();
        Debug.Log("Current Locale: " + currentLocale);

        // LocalizationSettings�̏���������������܂őҋ@
        await LocalizationSettings.InitializationOperation.Task;

        // ����ݒ�̓K�p
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
        SaveSettings(); // �ݒ��ύX�����Ƃ��ɕۑ�����
    }

    public string GetLanguage()
    {
        return currentLanguage;
    }

    // �ݒ��ۑ����郁�\�b�h
    public void SaveSettings()
    {
        PlayerPrefs.SetString("Language", currentLanguage);
        PlayerPrefs.Save();
    }

    // �ݒ�����[�h���郁�\�b�h
    public void LoadSettings()
    {
        currentLanguage = PlayerPrefs.GetString("Language", "ja"); // �f�t�H���g��"en"
    }
}
