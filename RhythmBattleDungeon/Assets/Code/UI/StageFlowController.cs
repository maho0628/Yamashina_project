using UnityEngine;
using UnityEngine.SceneManagement;

public class StageFlowController : MonoBehaviour
{



    private bool hasTransitioned = false;

    void Update()
    {
        if (hasTransitioned) return; // © ‘¦ return ‚Å‚±‚êˆÈãˆ—‚³‚¹‚È‚¢

        if (!hasTransitioned && AudioManager.Instance.IsBGMFinished())
        {

            hasTransitioned = true;
            Invoke("GoToResult", StageManager.Instance.GetCurrentStageConfig().DelayBeforeResult); // 2•b—]‰C‚ğæ‚Á‚Ä‚©‚ç‘JˆÚ
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
