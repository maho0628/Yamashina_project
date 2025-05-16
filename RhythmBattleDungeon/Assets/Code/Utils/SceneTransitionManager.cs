using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class SceneTransitionManager : SingletonMonoBehaviour<SceneTransitionManager>
{
    private Image fadeInstance;
    private bool isTransitioning = false;
    private SceneReference initialScene;

   
   

    // フェード用プレハブの設定
    public void SetFadePrefab(GameObject prefab)
    {
        if (fadeInstance == null && prefab != null)
        {
            var fadeObj = Instantiate(prefab);
            fadeInstance = fadeObj.GetComponentInChildren<Image>();
            fadeObj.GetComponent<Canvas>().sortingOrder = 100;
            fadeObj.GetComponent<Canvas>().renderMode = RenderMode.ScreenSpaceOverlay;
            fadeInstance.gameObject.SetActive(false);
            DontDestroyOnLoad(fadeObj);
        }
    }

    // 初期シーンをセットする関数
    public void SetInitialScene(SceneReference sceneReference)
    {
        initialScene = sceneReference;
    }

    // シーン遷移
    public void TransitionTo(SceneReference sceneReference)
    {
        if (isTransitioning || sceneReference == null) return;

        StartCoroutine(FadeOutAndLoadScene(sceneReference.sceneName));
    }

    private IEnumerator FadeOutAndLoadScene(string sceneName)
    {
        isTransitioning = true;

        fadeInstance.gameObject.SetActive(true);
        Color color = fadeInstance.color;
        color.a = 0;
        fadeInstance.color = color;

        while (fadeInstance.color.a < 1f)
        {
            color.a += Time.unscaledDeltaTime * GameInitializer.Instance.GetGameSettings().FadeSpeed;
            fadeInstance.color = color;
            yield return null;
        }

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
        while (!asyncLoad.isDone)
            yield return null;

        StartCoroutine(FadeIn());
    }

    private IEnumerator FadeIn()
    {
        Color color = fadeInstance.color;
        color.a = 1;
        fadeInstance.color = color;

        while (fadeInstance.color.a > 0f)
        {
            color.a -= Time.unscaledDeltaTime * GameInitializer.Instance.GetGameSettings().FadeSpeed;
            fadeInstance.color = color;
            yield return null;
        }

        fadeInstance.gameObject.SetActive(false);
        isTransitioning = false;
    }
}
