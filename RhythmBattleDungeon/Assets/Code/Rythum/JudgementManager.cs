using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// ノーツ判定を行うマネージャー
/// </summary>
public class JudgementManager : SingletonMonoBehaviour<JudgementManager>
{
    private List<JudgementConfig> judgementConfigs;

    public void Setup(List<JudgementConfig> configs)
    {
        judgementConfigs = new List<JudgementConfig>(configs);

        // Miss を含まない場合は fallback を追加
        if (!judgementConfigs.Exists(j => j.JudgementName == "Miss"))
        {
            Debug.LogWarning("[JudgementManager] Miss 判定が未登録、fallback を追加");
            judgementConfigs.Add(JudgementConfig.CreateFallbackMiss());
        }

        // 判定ウィンドウが小さい順に並び替え（Perfect → Great → Good → Miss）
        judgementConfigs.Sort((a, b) => a.MaxTimeDifference.CompareTo(b.MaxTimeDifference));
    }

    public JudgementConfig GetJudgement(float timeDifference)
    {
        foreach (var config in judgementConfigs)
        {
            if (Mathf.Abs(timeDifference) <= config.MaxTimeDifference)
                return config;
        }

        return null; // fallback で Miss を追加していれば null にはならない想定
    }

    public float GetMaxJudgementTime()
    {
            if (judgementConfigs == null || !judgementConfigs.Any())
        {
            Debug.LogError("[JudgementManager] 判定データが初期化されていません");
            return 0f;
        }
        return judgementConfigs.Max(j => j.MaxTimeDifference);
    }
    public JudgementConfig GetMissJudgement()
    {
        return judgementConfigs.FirstOrDefault(j => j.JudgementName == "Miss");
    }

    public List<JudgementConfig> GetAll() => new List<JudgementConfig>(judgementConfigs);
}

