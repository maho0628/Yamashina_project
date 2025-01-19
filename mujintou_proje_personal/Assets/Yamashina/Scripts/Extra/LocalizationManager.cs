using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class LocalizationManager : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(ApplyLocalizationToAllText());
    }

    IEnumerator ApplyLocalizationToAllText()
    {
        // �V�[����̂��ׂĂ�LocalizedText�R���|�[�l���g���擾
        var localizedTexts = FindObjectsOfType<Localization_initialize > ();

        // �eLocalizedText�R���|�[�l���g�Ń��[�J���C�Y��K�p
        foreach (var localizedText in localizedTexts)
        {
            yield return localizedText.StartCoroutine(localizedText.InitializeLocalization(localizedText.gameObject.name));
        }
    }
}


