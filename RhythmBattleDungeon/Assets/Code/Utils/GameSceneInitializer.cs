using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneInitializer : MonoBehaviour
{

    private void Awake()
    {

        if (!GameInitializer.Instance.Initialized)
        {
            GameInitializer.Instance.SetUpGameInitialize();
        }
       
    }


    private void Start()
    {
        if (StageManager.Instance.IsStageSelected)
        {
            var currentStage = StageManager.Instance.GetCurrentStageConfig();
            var judgementConfigs = currentStage.JudgementConfigs;
            Debug.Log(judgementConfigs.ToString()); 
            JudgementManager.Instance.Setup(judgementConfigs);
            FindAnyObjectByType<InputHandler>()?.Initialize();


            if (currentStage != null)
            {
                // ステージ用のBGMがあるならそれを再生
                AudioManager.Instance.ForcePlayBGM(currentStage.StageBgm.BgmId);

                Debug.Log($"[GameSceneInitializer] ステージ用BGMを再生: {currentStage.StageBgm.BgmId}");
            }

        }
        else
        {
            string sceneName = SceneManager.GetActiveScene().name;
            SceneBGMConfigTable sceneBgmConfigTable = GameInitializer.Instance.GetSceneBGMConfigTable();
            string bgmId = sceneBgmConfigTable.GetSceneBgmConfigName(sceneName);
            Debug.Log($"シーン名: {sceneName}, BGM ID: {bgmId}");
            AudioManager.Instance.PlayBGMIfNotPlaying(bgmId);
            Debug.LogWarning("ステージがまだ選択されていません！");
            return;
        }

    }

}
