using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using UnityEngine.Localization.Tables;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.ResourceManagement.AsyncOperations;

public class Localization_initialize : MonoBehaviour
{



    public Text uiText;  // UI�̃e�L�X�g�R���|�[�l���g
    private string tableName = "Local_Ja_toEn";  // ���[�J���C�Y�e�[�u���̖��O
    void Start()
    {

        // GameObject�̖��O���G���g���[�L�[�Ƃ��Ďg�p
        string entryKey = gameObject.name;

        // ���[�J���C�Y���ꂽ�e�L�X�g��񓯊��Ŏ擾
        StartCoroutine(InitializeLocalization(entryKey));
    }

   public IEnumerator InitializeLocalization(string entryKey)
    {
      // LocalizationSettings�������������܂őҋ@
        yield return LocalizationSettings.InitializationOperation;

        // ���[�J���C�Y���ꂽ�������񓯊��Ŏ擾
        var localizedString = LocalizationSettings.StringDatabase.GetLocalizedStringAsync(tableName, entryKey);
        yield return localizedString;

        // �e�L�X�g��ݒ�
        if (localizedString.Status == AsyncOperationStatus.Succeeded)
        {
            uiText.text = localizedString.Result;  // ���������ꍇ�A�e�L�X�g��ݒ�
        }
        else
        {
            Debug.LogError($"Localization failed for key: {entryKey} in table: {tableName}");
            uiText.text = "";  // �G���[�̏ꍇ�A�e�L�X�g����ɐݒ�
        }
    }
}

       

    







