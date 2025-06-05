using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleManager : MonoBehaviour
{
    [SerializeField, Header("スタートボタン、設定されていない場合自動で設定")]
    private Button titleButton;
    [SerializeField, Header("終了ボタン、設定されていない場合自動で設定")]
    private Button quitButton;
    /// <summary>
    ///  GameInitializer.Instanceがイニシャライズされるまで待つ
    /// </summary>

    private void Start()
    {
        titleButton = GameObject.Find("TitleButton").GetComponent<Button>();
        quitButton = GameObject.Find("QuitButton").GetComponent<Button>();
        quitButton.onClick.AddListener(()=>GameExitManager.Instance.InitializeConfirmSettings());   
        //
        OnTitleButtonClicked();
    }


    private void OnTitleButtonClicked()
    {

        SceneDatabase sceneDatabase = GameInitializer.Instance.GetSceneDatabase();
        string currentSceneName = SceneManager.GetActiveScene().name;

        SceneReference nextScene = sceneDatabase.GetNextScene(currentSceneName);

        titleButton.onClick.AddListener(() => SceneTransitionManager.Instance.TransitionTo(nextScene));
    }



}
