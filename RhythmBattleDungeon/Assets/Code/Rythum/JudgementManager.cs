using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ノーツ判定を行うマネージャー
/// </summary>
public class JudgementManager : SingletonMonoBehaviour<JudgementManager>
{
    private List<JudgementConfig> judgementConfigs;

    
    /// <summary>
    /// ステージの設定に応じた判定データをセットアップ
    /// </summary>
    public void Setup(List<JudgementConfig> configs)
    {
        if (configs == null || configs.Count == 0)
        {
            Debug.LogError("[JudgementManager] 判定設定が渡されていません！");
            return;
        }

        judgementConfigs = configs;
    }

    /// <summary>
    /// 指定ノーツの判定結果を返す
    /// </summary>
    public JudgementConfig GetJudgement(float timeDifference)
    {
        foreach (var config in judgementConfigs)
        {
            if (Mathf.Abs(timeDifference) <= config.MaxTimeDifference)
            {
                return config;
            }
        }

        return null; // Miss などに対応した fallback を作ってもいい
    }


    public List<JudgementConfig> GetJudgementsWithFallback()
    {
        var listCopy = new List<JudgementConfig>(judgementConfigs);

        // Miss が含まれているかチェック
        bool hasMiss = listCopy.Exists(j => j.JudgementName == "Miss");

        if (!hasMiss)
        {
            Debug.LogWarning("[JudgementConfigTable] Miss 判定が設定されていません。デフォルトを追加します。");

            // フォールバック用 Miss 判定を追加
            var fallbackMiss = new JudgementConfig();

            typeof(JudgementConfig).GetField("judgementName", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
      ?.SetValue(fallbackMiss, "Miss");

            typeof(JudgementConfig).GetField("maxTimeDifference", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                ?.SetValue(fallbackMiss, 999f);

            typeof(JudgementConfig).GetField("displayColor", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                ?.SetValue(fallbackMiss, Color.gray);

            listCopy.Add(fallbackMiss);
        }

        return listCopy;
    }

}
