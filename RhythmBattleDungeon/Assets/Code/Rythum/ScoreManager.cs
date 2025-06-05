using System;
using System.Collections;
using System.Linq;
using UnityEditor.TerrainTools;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class ScoreManager : SingletonMonoBehaviour<ScoreManager>
{
    private int currentScore = 0;
    private int maxScore = 0;

    public event Action<int> OnScoreChanged;

    private void Start()
    {

        StartCoroutine(WaitAndSubscribe());

    }

    private IEnumerator WaitAndSubscribe()
    {
        Debug.LogError("[ScoreManager] WaitAndSubscribe START!!!");

        int waitCount = 0;
        while (NoteManager.Instance == null || !NoteManager.Instance.IsInitialized)
        {
            waitCount++;
            if (waitCount % 60 == 0) // 1秒ごとに表示
            {
                Debug.Log($"[ScoreManager] Still waiting... {waitCount} frames (NoteManager: {NoteManager.Instance != null}, IsInitialized: {NoteManager.Instance?.IsInitialized})");
            }
            yield return null;
        }

        Debug.LogError("[ScoreManager] NoteManager ready! Subscribing to events...");

        Debug.Log($"[ScoreManager] NotesSpawned status: {NoteManager.Instance.NotesSpawned}");

        if (NoteManager.Instance.NotesSpawned)
        {
            Debug.LogError("[ScoreManager] Calling CalculateMaxScore manually!!!");
        }
        else
        {
            Debug.LogError("[ScoreManager] NotesSpawned is FALSE, waiting for event...");
        }

        Debug.LogError("[ScoreManager] WaitAndSubscribe COMPLETE!!!");
    }
    public void Initialize()
    {
        currentScore = 0;
    }

    public void AddScore(int score)
    {
        currentScore += score;
        OnScoreChanged?.Invoke(currentScore);

        // UI�X�V������΂����ŌĂ�
    }

    public int GetCurrentScore() => currentScore;
    public int GetMaxScore() => maxScore;





    public void CalculateMaxScore()
    {
        Debug.Log($"[ScoreManager] CalculateMaxScore() START - Current maxScore: {maxScore}");

        if (maxScore > 0)
        {
            Debug.Log($"[ScoreManager] 既に maxScore 計算済みのためスキップ (maxScore: {maxScore})");
            return;
        }

        Debug.Log("[ScoreManager] CalculateMaxScore 呼ばれた");

        if (NoteManager.Instance == null)
        {
            Debug.LogError("[ScoreManager] NoteManager.Instance is null!");
            return;
        }

        var config = StageManager.Instance.GetCurrentStageConfig();
        Debug.Log($"[ScoreManager] StageConfig: {(config != null ? "Found" : "NULL")}"); // ←追加

        if (config == null)
        {
            Debug.LogError("[ScoreManager] StageConfig is null!");
            return;
        }

        Debug.Log($"[ScoreManager] JudgementConfigs count: {config.JudgementConfigs?.Count?? 0}"); // ←追加

        var perfectConfig = config?.JudgementConfigs.FirstOrDefault(j => j.Logic.SetJudgementName == "Perfect");
        Debug.Log($"[ScoreManager] PerfectConfig: {(perfectConfig != null ? "Found" : "NULL")}"); // ←追加

        if (perfectConfig == null)
        {
            Debug.LogError("[ScoreManager] JudgementConfig で 'Perfect' 設定が見つかりません");
            // 利用可能な判定名を表示
            if (config.JudgementConfigs != null)
            {
                foreach (var judgement in config.JudgementConfigs)
                {
                    Debug.Log($"[ScoreManager] Available judgement: {judgement.Logic.SetJudgementName}");
                }
            }
            return;
        }

        int bestScore = perfectConfig.Logic.SetScoreValue;
        int totalNotes = NoteManager.Instance.TotalNoteCount;

        Debug.Log($"[ScoreManager] bestScore: {bestScore}, totalNotes: {totalNotes}"); // ←追加

        maxScore = bestScore * totalNotes;

        Debug.Log($"[ScoreManager] MaxScore calculated: {maxScore} (bestScore: {bestScore}, totalNotes: {totalNotes})");
    }
    public float GetScoreRate()
    {
        if (maxScore == 0) return 0f;
        return (float)currentScore / maxScore;
    }


}
