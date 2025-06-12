using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class RetryManager : SingletonMonoBehaviour<RetryManager>
{
    #region リトライ用の状態管理変数

    private string lastPlayedStageId;
    private bool canRetry = false;

    #endregion

    #region プロパティ

    public bool CanRetry => canRetry;
    public string LastPlayedStageId => lastPlayedStageId;

    #endregion

    #region ゲーム開始時の情報保存

    public void SaveGameStartInfo(string stageId)
    {
        lastPlayedStageId = stageId;
        canRetry = true;

        DebugManager.Log($"[RetryManager] ゲーム開始情報を保存: StageID = {stageId}");
    }

    public void ClearRetryInfo()
    {
        lastPlayedStageId = null;
        canRetry = false;

        DebugManager.Log("[RetryManager] リトライ情報をクリア");
    }

    #endregion

    #region リトライ実行メソッド

    public void StartDirectRetry()
    {
        if (!canRetry || string.IsNullOrEmpty(lastPlayedStageId))
        {
            DebugManager.LogError("[RetryManager] リトライ情報が不正です");
            return;
        }

        StartCoroutine(DirectRetryCoroutine());
    }

    private IEnumerator DirectRetryCoroutine()
    {
        DebugManager.Log($"[RetryManager] 直接リトライ開始: {lastPlayedStageId}");

        ResetAllGameManagers();

        var stageTable = StageManager.Instance.GetStageConfigTable();
        if (stageTable != null)
        {
            StageManager.Instance.SetupStage(stageTable, lastPlayedStageId);
        }

        yield return StartCoroutine(RestartGameSceneCoroutine());
    }

    public void StartRetryFromSongSelect()
    {
        if (!canRetry)
        {
            DebugManager.LogError("[RetryManager] リトライ情報が不正です");
            return;
        }

        StartCoroutine(RetryFromSongSelectCoroutine());
    }

    private IEnumerator RetryFromSongSelectCoroutine()
    {
        DebugManager.Log("[RetryManager] 選曲画面経由でリトライ");
        yield return StartCoroutine(LoadSongSelectSceneCoroutine());
    }

    public void StartReturnToTitle()
    {
        StartCoroutine(ReturnToTitleCoroutine());
    }

    private IEnumerator ReturnToTitleCoroutine()
    {
        DebugManager.Log("[RetryManager] タイトル画面に戻る");

        ClearRetryInfo();

        yield return StartCoroutine(ReturnToPrePreviousSceneCoroutine());
    }

    private IEnumerator ReturnToPrePreviousSceneCoroutine()
    {
        var sceneDatabase = GameInitializer.Instance.GetSceneDatabase();
        string currentSceneName = SceneManager.GetActiveScene().name;
        var nextScene = sceneDatabase.GetNextScene(currentSceneName);

        StageManager.Instance.SetStageSelected(false);

        SceneTransitionManager.Instance.TransitionTo(nextScene);
        ResetAllGameManagers();

        yield return new WaitUntil(() => !SceneTransitionManager.Instance.IsTransitioning);

        DebugManager.Log("[RetryManager] 2つ前のシーンへ遷移完了");
    }

    #endregion

    #region 内部メソッド - ゲーム状態リセット

    private void ResetAllGameManagers()
    {
        DebugManager.Log("[RetryManager] 全マネージャーをリセット中...");

        ScoreManager.Instance?.Initialize();
        ComboManager.Instance?.ResetAll();
        AudioManager.Instance?.StopBGM();
        UIManager.Instance?.StopReadyGo();
        JudgementManager.Instance.ResetAllJudgement();  
        var scoreGauge = FindAnyObjectByType<ScoreGaugeUI>();
        if (scoreGauge != null)
        {
            scoreGauge.ResetGauge();
        }
        DebugManager.Log("[RetryManager] 全マネージャーのリセット完了");
    }

    private IEnumerator RestartGameSceneCoroutine()
    {
        DebugManager.Log("[RetryManager] ゲームシーン再開処理");

        var delayMs = GameInitializer.Instance.GetGameSettings().RetryDelayMilliseconds;
        yield return new WaitForSecondsRealtime(delayMs);

        NoteManager.Instance?.Initialize();

        var sceneDatabase = GameInitializer.Instance.GetSceneDatabase();
        string currentSceneName = SceneManager.GetActiveScene().name;

        var currentScene = sceneDatabase.GetScene(currentSceneName);   
        SceneTransitionManager.Instance.TransitionTo(currentScene);

        DebugManager.Log("[RetryManager] ゲームシーン再開完了");
    }

    #endregion

    #region シーン遷移

    private IEnumerator LoadSongSelectSceneCoroutine()
    {
        DebugManager.Log("[RetryManager] 選曲シーン読み込み中...");

        var sceneDatabase = GameInitializer.Instance.GetSceneDatabase();
        string currentSceneName = SceneManager.GetActiveScene().name;
        var previousScene = sceneDatabase.GetPreviousScene(currentSceneName);
        var prePreviousScene = sceneDatabase.GetPreviousScene(previousScene);

        StageManager.Instance.SetStageSelected(false);

        SceneTransitionManager.Instance.TransitionTo(prePreviousScene);
        ResetAllGameManagers();
        yield return new WaitUntil(() => !SceneTransitionManager.Instance.IsTransitioning);
        

        DebugManager.Log("[RetryManager] 選曲シーン読み込み完了");
    }

    #endregion

    #region デバッグ・ユーティリティ

    [System.Diagnostics.Conditional("UNITY_EDITOR")]
    public void DebugPrintRetryState()
    {
        DebugManager.Log("[RetryManager] === リトライ状態 ===");
        DebugManager.Log($"CanRetry: {canRetry}");
        DebugManager.Log($"LastPlayedStageId: {lastPlayedStageId ?? "NULL"}");
        DebugManager.Log("=========================");
    }

    #endregion
}