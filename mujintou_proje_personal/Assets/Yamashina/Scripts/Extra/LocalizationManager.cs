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
        // シーン上のすべてのLocalizedTextコンポーネントを取得
        var localizedTexts = FindObjectsOfType<Localization_initialize > ();

        // 各LocalizedTextコンポーネントでローカライズを適用
        foreach (var localizedText in localizedTexts)
        {
            yield return localizedText.StartCoroutine(localizedText.InitializeLocalization(localizedText.gameObject.name));
        }
    }
}


