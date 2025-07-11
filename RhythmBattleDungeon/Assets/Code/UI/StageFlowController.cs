using UnityEngine;
using UnityEngine.SceneManagement;

public class StageFlowController : MonoBehaviour
{



    private bool hasTransitioned = false;

    void Update()
    {
        if (hasTransitioned) return; // Å© ë¶ return Ç≈Ç±ÇÍà»è„èàóùÇ≥ÇπÇ»Ç¢

        if (!hasTransitioned && AudioManager.Instance.IsBGMFinished())
        {

            hasTransitioned = true;
            AudioManager.Instance.PlaySEById(SEName.ToResult);
            Invoke("GoToResult", StageManager.Instance.GetCurrentStageConfig().DelayBeforeResult); // 
        }
    }

    private void GoToResult()
    {
        var sceneDatabase = GameInitializer.Instance.GetSceneDatabase();
        string currentSceneName = SceneManager.GetActiveScene().name;
       
        var nextScene = sceneDatabase.GetNextScene(currentSceneName);
        StageManager.Instance.SetStageSelected(false);
        SceneTransitionManager.Instance.TransitionTo(nextScene);
    }
}
