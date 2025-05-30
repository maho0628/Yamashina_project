using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleManager : MonoBehaviour
{

    private Button titleButton;

    /// <summary>
    ///  GameInitializer.Instanceがイニシャライズされるまで待つ
    /// </summary>

    private void Start()
    {
        titleButton = GameObject.Find("TitleButton").GetComponent<Button>();

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
