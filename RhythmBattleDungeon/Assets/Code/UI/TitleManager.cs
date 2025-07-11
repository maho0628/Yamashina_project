using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleManager : MonoBehaviour
{
    [SerializeField, Header("スタートボタン、設定されていない場合自動で設定")]
    private Button titleButton;

    [SerializeField, Header("終了ボタン、設定されていない場合自動で設定")]
    private Button quitButton;

    private void Start()
    {
        InitializeButtons();
        SetupButtonListeners();
    }

    /// <summary>
    /// ボタンの初期化を行う
    /// </summary>
    private void InitializeButtons()
    {
        // SerializeFieldで設定されていない場合は自動で取得
        if (titleButton == null)
        {
            titleButton = FindButtonByName("TitleButton");
        }

        if (quitButton == null)
        {
            quitButton = FindButtonByName("QuitButton");
        }
    }

    /// <summary>
    /// ボタンのリスナーを設定する
    /// </summary>
    private void SetupButtonListeners()
    {
        if (titleButton != null)
        {
            titleButton.onClick.AddListener(OnTitleButtonClicked);
        }
        else
        {
            Debug.LogError("TitleButton not found!");
        }

        if (quitButton != null)
        {
            quitButton.onClick.AddListener(OnQuitButtonClicked);
        }
        else
        {
            Debug.LogError("QuitButton not found!");
        }
    }

    /// <summary>
    /// 指定された名前のボタンを検索して返す
    /// </summary>
    /// <param name="buttonName">検索するボタンの名前</param>
    /// <returns>見つかったボタン、見つからない場合はnull</returns>
    private Button FindButtonByName(string buttonName)
    {
        GameObject buttonObject = GameObject.Find(buttonName);
        if (buttonObject != null)
        {
            return buttonObject.GetComponent<Button>();
        }

        Debug.LogWarning($"Button '{buttonName}' not found in scene.");
        return null;
    }

    /// <summary>
    /// タイトルボタンがクリックされた時の処理
    /// </summary>
    private void OnTitleButtonClicked()
    {
        try
        {
            // 次のシーンを取得
            var sceneDatabase = GameInitializer.Instance.GetSceneDatabase();
            string currentSceneName = SceneManager.GetActiveScene().name;
            var nextScene = sceneDatabase.GetNextScene(currentSceneName);

            // シーン遷移を実行
            SceneTransitionManager.Instance.TransitionTo(nextScene);

            // SE再生
            PlayTitleClickSE();
        }
        catch (System.Exception e)
        {
            Debug.LogError($"Error in OnTitleButtonClicked: {e.Message}");
        }
    }

    /// <summary>
    /// タイトルクリック用SEを再生する
    /// </summary>
    private void PlayTitleClickSE()
    {
        AudioManager.Instance.PlaySEById(SEName.TitleClicked);
    }

    /// <summary>
    /// 終了ボタンがクリックされた時の処理
    /// </summary>
    private void OnQuitButtonClicked()
    {
        try
        {
            // 終了確認設定を初期化
            GameExitManager.Instance.InitializeConfirmSettings();

            // SE再生
            PlayTitleClickSE();
        }
        catch (System.Exception e)
        {
            Debug.LogError($"Error in OnQuitButtonClicked: {e.Message}");
        }
    }

    /// <summary>
    /// オブジェクトが破棄される際にリスナーを解除
    /// </summary>
    private void OnDestroy()
    {
        if (titleButton != null)
        {
            titleButton.onClick.RemoveListener(OnTitleButtonClicked);
        }

        if (quitButton != null)
        {
            titleButton.onClick.RemoveListener(OnQuitButtonClicked);
        }
    }
}