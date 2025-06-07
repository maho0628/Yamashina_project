using System.Collections;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneInitializer : MonoBehaviour
{
    private StageConfig currentStage;

    private void Awake()
    {

        if (!GameInitializer.Instance.Initialized)
        {
            GameInitializer.Instance.SetUpGameInitialize();
        }
        if (StageManager.Instance.IsStageSelected)
        {
            NoteManager.Instance?.ResetForNewScene();
            AnimationManager.Instance.InitEffectController();   

        }

    }


    private void Start()
    {

        if (StageManager.Instance.IsStageSelected)
        {
            currentStage = StageManager.Instance.GetCurrentStageConfig();

            var judgementConfigs = currentStage.JudgementConfigs;
            DebugManager.Log(judgementConfigs.ToString());
            JudgementManager.Instance.Setup(judgementConfigs);
            if (currentStage != null)
            {
                // ステージ用のBGMがあるならそれを再生

                AudioManager.Instance.ForcePlayBGM(currentStage.StageBgmId   );

                DebugManager.Log($"[GameSceneInitializer] ステージ用BGMを再生: {currentStage.StageBgm.BgmId}");

            }
            StartCoroutine(WaitForBGMThenInitialize());






        }
        else
        {
            string sceneName = SceneManager.GetActiveScene().name;
            SceneBGMConfigTable sceneBgmConfigTable = GameInitializer.Instance.GetSceneBGMConfigTable();
            BGMName bgmId = sceneBgmConfigTable.GetSceneBgmConfigName(sceneName);
            DebugManager.Log($"シーン名: {sceneName}, BGM ID: {bgmId}");
            AudioManager.Instance.PlayBGMIfNotPlaying(bgmId);
            DebugManager.LogWarning("ステージがまだ選択されていません！");

            return;
        }

    }
    private IEnumerator WaitForBGMThenInitialize()
    {
        var scrollDuration = currentStage.ScrollConfig.GetNoteTimingConfig().ScrollDuration;

        NoteManager.Instance.Initialize();
        yield return UIManager.Instance.ShowReadyGoAsync().ToCoroutine();
        yield return new WaitUntil(() => AudioManager.Instance.GetCurrentBGMTime() > scrollDuration);

        NoteManager.Instance.AllowNoteSpawning();
        ScoreManager.Instance.CalculateMaxScore();  
        FindAnyObjectByType<InputHandler>()?.InitializeInput();
    }


 

}
