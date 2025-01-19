using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.UI;

public class Debug_TextChanger : MonoBehaviour
{
    private LocalizedTextAssetLoader localization;
    private void Start()
    {
        localization = GameObject.FindAnyObjectByType<LocalizedTextAssetLoader>();
    }

    // Update is called once per frame
    void Update()
    {
        string currentLocale = LocalizationSettings.SelectedLocale.Identifier.Code;
        if (currentLocale == "en")
        {
            localization.LoadTextAssets(localization.englishFolderPath);
            TextAsset textAsset = Resources.Load<TextAsset>(localization.englishFolderPath);
           GameObject.FindAnyObjectByType<Text>().text = textAsset.text;    
        }
    }
}