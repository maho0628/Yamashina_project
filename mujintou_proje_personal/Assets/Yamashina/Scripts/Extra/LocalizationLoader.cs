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

        // プリロードが完了したらメインシーンをロード
        SceneManager.LoadScene(SceneManager.GetActiveScene().ToString());
    }
}


