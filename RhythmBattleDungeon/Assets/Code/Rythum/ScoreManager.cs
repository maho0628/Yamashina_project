using System.Collections;
using System.Linq;
using UnityEditor.TerrainTools;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class ScoreManager : SingletonMonoBehaviour<ScoreManager>
{
    private int currentScore = 0;
    private int maxScore = 0;


    private void Start()
    {
        StartCoroutine(WaitAndSubscribe());
    }

    private IEnumerator WaitAndSubscribe()
    {
        while (NoteManager.Instance == null)

            // フレーム跨いで NoteManager の Start が完了するのを待つ
            yield return null;

        NoteManager.Instance.OnNotesSpawned += CalculateMaxScore;

        if (NoteManager.Instance.NotesSpawned)
        {
            Debug.Log("[ScoreManager] ノーツは既に生成されていたため、即座にスコアを計算します。");
            CalculateMaxScore();
        }

        Debug.Log("[ScoreManager] イベント購読完了");
    }

    public void Initialize()
    {
        currentScore = 0;
    }

    public void AddScore(int score)
    {
        currentScore += score;
        // UI更新があればここで呼ぶ
        //ScoreUI.Instance?.UpdateScore(currentScore, maxScore);
    }

    public int GetCurrentScore() => currentScore;
    public int GetMaxScore() => maxScore;





    public void CalculateMaxScore()
    {
        Debug.Log("[ScoreManager] CalculateMaxScore 呼ばれた");

        var config = StageManager.Instance.GetCurrentStageConfig();
        var perfectConfig = config?.JudgementConfigs.FirstOrDefault(j => j.JudgementName == "Perfect");

        if (perfectConfig == null)
        {
            Debug.LogError("[ScoreManager] JudgementConfig に 'Perfect' 判定が存在しません");
            throw new System.Exception("Perfect 判定が存在しないため、スコア計算ができません");
        }
        int bestScore = perfectConfig.ScoreValue;
        int totalNotes = NoteManager.Instance.TotalNoteCount;

        maxScore = bestScore * totalNotes;

    }
    public float GetScoreRate()
    {
        if (maxScore == 0) return 0f;
        return (float)currentScore / maxScore;
    }


}
