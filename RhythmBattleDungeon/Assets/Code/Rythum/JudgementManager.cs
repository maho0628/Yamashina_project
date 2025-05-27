using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class JudgementManager : SingletonMonoBehaviour<JudgementManager>
{
    private List<JudgementConfig> judgementConfigs;

    private Dictionary<string, int> judgementCounts = new();

    public event Action OnJudgementApplied;

    /// <summary>
    /// �O�����画������Z�b�g�A�b�v
    /// </summary>
    public void Setup(List<JudgementConfig> configs)
    {
        judgementConfigs = new List<JudgementConfig>(configs);

        // Miss ���܂܂Ȃ��ꍇ�� fallback ��ǉ�
        if (!judgementConfigs.Exists(j => j.Logic.SetJudgementName == "Miss"))
        {
            Debug.LogWarning("[JudgementManager] Miss ���肪���o�^�Afallback ��ǉ�");
            judgementConfigs.Add(JudgementConfig.CreateFallbackMiss());
        }

        // ����E�B���h�E�����������ɕ��ёւ��iPerfect �� Great �� Good �� Miss�j
        judgementConfigs.Sort((a, b) => a.Logic.SetMaxTimeDifference.CompareTo(b.Logic.SetMaxTimeDifference));
    }

    /// <summary>
    /// �S���胊�X�g���擾
    /// </summary>
    public List<JudgementConfig> GetAllJudgements() => judgementConfigs;

    /// <summary>
    /// �~�X����i�ő厞�ԁj���擾
    /// </summary>
    public JudgementConfig GetMissJudgement()
    {
        var miss = judgementConfigs.FirstOrDefault(j => j.Logic.JudgementName == "Miss");
        if (miss == null)
        {
            Debug.LogError("[JudgementManager] Miss ���肪�擾�ł��܂���ł���");
        }
        return miss;
    }

    /// <summary>
    /// ���̓^�C�~���O�ɉ����������Ԃ�
    /// </summary>
    public JudgementConfig EvaluateTiming(float timeDifference)
    {
        foreach (var judgement in judgementConfigs)
        {
            if (Mathf.Abs(timeDifference) <= judgement.Logic.SetMaxTimeDifference)
            {
                return judgement;
            }
        }
        // �����܂ŗ�����Miss
        return GetMissJudgement();
    }

    public float GetMaxJudgementTime()
    {
        if (judgementConfigs == null || !judgementConfigs.Any())
        {
            Debug.LogError("[JudgementManager] ����f�[�^������������Ă��܂���");
            return 0f;
        }
        return judgementConfigs.Max(j => j.Logic.SetMaxTimeDifference);
    }
    /// <summary>
    /// �X�R�A�A�R���{�A�G�t�F�N�g�Ȃǂ̏���
    /// </summary>
    public void ApplyJudgement(JudgementConfig config, int laneNumber)
    {
        if (!judgementCounts.ContainsKey(config.Logic.JudgementName))
            judgementCounts[config.Logic.JudgementName] = 0;

        judgementCounts[config.Logic.JudgementName]++;
        OnJudgementApplied?.Invoke();
        Debug.Log(config.Logic.JudgementName.ToString());   
        // �X�R�A����
        ScoreManager.Instance.AddScore(config.Logic.SetScoreValue);

        // �R���{����
        if (config.Logic.SetBreaksCombo)
        {
            ComboManager.Instance.ResetCombo();
        }
        else
        {
            ComboManager.Instance.IncrementCombo();
        }

        // ���o����
    }
    public int GetJudgementCount(string label)
    {
        return judgementCounts.TryGetValue(label, out int count) ? count : 0;
    }
}
