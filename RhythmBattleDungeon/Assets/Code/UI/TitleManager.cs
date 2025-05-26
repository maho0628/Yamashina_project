using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleManager : MonoBehaviour
{

    private Button titleButton;

    
    
    private void Awake()
    {
        titleButton = GameObject.Find("TitleButton").GetComponent<Button>();
        
        //
        NextScene();
    }


    private void NextScene()
    {

        SceneDatabase sceneDatabase = GameInitializer.Instance.GetSceneDatabase();
        string currentSceneName = SceneManager.GetActiveScene().name;

        SceneReference nextScene = sceneDatabase.GetNextScene(currentSceneName);

        titleButton.onClick.AddListener(() => SceneTransitionManager.Instance.TransitionTo(nextScene));
    }



}
