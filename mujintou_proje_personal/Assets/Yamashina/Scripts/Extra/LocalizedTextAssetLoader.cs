using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Localization;
using System.Collections.Generic;
using System.IO;

using UnityEngine.Localization.Settings;

public class LocalizedTextAssetLoader : MonoBehaviour
{
    public string japaneseFolderPath = "JapaneseTextAssets"; // ���{��e�L�X�g�̑��΃t�H���_�p�X
    public string englishFolderPath = "EnglishTextAssets"; // �p��e�L�X�g�̑��΃t�H���_�p�X
    public string targetFolderName = "TargetFolder"; // �T���������̃t�H���_��

    private List<TextAsset> textAssets = new List<TextAsset>();

    private void Start()
    {
        //LoadTextAssetsForCurrentLocale();
    }
    // ���݂̃��P�[���Ɋ�Â��ăe�L�X�g�A�Z�b�g�����[�h���郁�\�b�h
    public List<TextAsset> LoadTextAssetsForCurrentLocale()
    {
        Debug.Log(LocalizationSettings.SelectedLocale.Identifier.Code);
        textAssets.Clear();

        string currentLocale = LocalizationSettings.SelectedLocale.Identifier.Code;

        if (currentLocale == "ja")
        {
            LoadTextAssets(japaneseFolderPath);
        }
        else if (currentLocale =="en")
        {
            LoadTextAssets(englishFolderPath);
        }
        
        return textAssets;
    }

    public void LoadTextAssets(string relativeFolderPath)
    {
        string absolutePath = Path.Combine(Application.dataPath, "Resources", relativeFolderPath);
        Debug.Log(absolutePath);

        if (Directory.Exists(absolutePath))
        {
            string targetPath = FindTargetFolder(absolutePath);
            Debug.Log(targetPath);
            if (targetPath != null)
            {
                Debug.Log(targetPath);
                Debug.Log(relativeFolderPath);
                LoadTextAssetsFromPath(targetPath,relativeFolderPath);
            }
            else
            {
                Debug.LogError($"Target folder '{targetFolderName}' not found.");
            }
        }
        else
        {
            Debug.LogError($"Directory not found: {absolutePath}");
        }
    }

    private string FindTargetFolder(string path)
    {
        if (Path.GetFileName(path) == targetFolderName)
        {
            return path;
        }

        string[] subDirectories = Directory.GetDirectories(path);
        Debug.Log(subDirectories.Length);
        foreach (string subDir in subDirectories)
        {
            string result = FindTargetFolder(subDir);
            if (result != null)
            {
                return result;
            }
        }

        return null;
    }

    public void LoadTextAssetsFromPath(string path,string relativeFolderPath)
    {
        string[] files = Directory.GetFiles(path, "*.txt");

        foreach (string file in files)
        {
            string fileName = Path.GetFileNameWithoutExtension(file);
            print(fileName);

            string resourcePath = Path.Combine(relativeFolderPath, targetFolderName, fileName);
            TextAsset textAsset = Resources.Load<TextAsset>(resourcePath);
            if (textAsset != null)
            {
                textAssets.Add(textAsset);
                Debug.Log(textAsset);
            }
            else
            {
                Debug.LogError($"Failed to load TextAsset: {resourcePath}");
            }
        }
    }
   
}



