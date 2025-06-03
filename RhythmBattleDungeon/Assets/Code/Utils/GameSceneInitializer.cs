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

    }


    private void Start()
    {

        if (StageManager.Instance.IsStageSelected)
        {
            currentStage = StageManager.Instance.GetCurrentStageConfig();

            var judgementConfigs = currentStage.JudgementConfigs;
            Debug.Log(judgementConfigs.ToString());
            JudgementManager.Instance.Setup(judgementConfigs);
            if (currentStage != null)
            {
                // ステージ用のBGMがあるならそれを再生

                AudioManager.Instance.ForcePlayBGM(currentStage.StageBgm.BgmId);

                Debug.Log($"[GameSceneInitializer] ステージ用BGMを再生: {currentStage.StageBgm.BgmId}");

            }
            StartCoroutine(WaitForBGMThenInitialize());






        }
        else
        {
            string sceneName = SceneManager.GetActiveScene().name;
            SceneBGMConfigTable sceneBgmConfigTable = GameInitializer.Instance.GetSceneBGMConfigTable();
            string bgmId = sceneBgmConfigTable.GetSceneBgmConfigName(sceneName);
            Debug.Log($"�V�[����: {sceneName}, BGM ID: {bgmId}");
            AudioManager.Instance.PlayBGMIfNotPlaying(bgmId);
            Debug.LogWarning("ステージがまだ選択されていません！");

            return;
        }

    }
    private IEnumerator WaitForBGMThenInitialize()
    {
        var scrollDuration = currentStage.ScrollConfig.ScrollDuration;
        yield return new WaitUntil(() => AudioManager.Instance.GetCurrentBGMTime() > scrollDuration);

        NoteManager.Instance.Initialize();
        yield return UIManager.Instance.ShowReadyGoAsync().ToCoroutine();
        NoteManager.Instance.AllowNoteSpawning();

        Debug.Log("NoteManager ��������");

        FindAnyObjectByType<InputHandler>()?.InitializeInput();
    }


 

}
