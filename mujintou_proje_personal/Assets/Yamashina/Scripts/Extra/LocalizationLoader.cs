using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Localization.Settings;
using System.Collections;

public class LocalizationLoader : MonoBehaviour
{
  
    void Start()
    {
        StartCoroutine(PreloadLocalization());
    }

    IEnumerator PreloadLocalization()
    {
        yield return LocalizationSettings.InitializationOperation;

        // �v�����[�h�����������烁�C���V�[�������[�h
        SceneManager.LoadScene(SceneManager.GetActiveScene().ToString());
    }
}


