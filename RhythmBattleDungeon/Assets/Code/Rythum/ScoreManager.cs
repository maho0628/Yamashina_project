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
        while (NoteManager.Instance == null || !NoteManager.Instance.IsInitialized)
            yield return null;

        NoteManager.Instance.OnNotesSpawned += CalculateMaxScore;

        if (NoteManager.Instance.NotesSpawned)
        {
            Debug.Log("[ScoreManager] �m�[�c�͊��ɐ�������Ă������߁A�����ɃX�R�A���v�Z���܂��B");
            CalculateMaxScore();
        }

        Debug.Log("[ScoreManager] �C�x���g�w�Ǌ���");
    }

    public void Initialize()
    {
        currentScore = 0;
    }

    public void AddScore(int score)
    {
        currentScore += score;
        // UI�X�V������΂����ŌĂ�
        //ScoreUI.Instance?.UpdateScore(currentScore, maxScore);
    }

    public int GetCurrentScore() => currentScore;
    public int GetMaxScore() => maxScore;





    public void CalculateMaxScore()
    {
        if (maxScore > 0)
        {
            Debug.Log("[ScoreManager] ���ł� maxScore �v�Z�ς݂̂��߃X�L�b�v");
            return;
        }
        Debug.Log("[ScoreManager] CalculateMaxScore �Ă΂ꂽ");

        var config = StageManager.Instance.GetCurrentStageConfig();
        var perfectConfig = config?.JudgementConfigs.FirstOrDefault(j => j.JudgementName == "Perfect");

        if (perfectConfig == null)
        {
            Debug.LogError("[ScoreManager] JudgementConfig �� 'Perfect' ���肪���݂��܂���");
            throw new System.Exception("Perfect ���肪���݂��Ȃ����߁A�X�R�A�v�Z���ł��܂���");
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
