using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// ノーツ判定（Perfect / Great / Good / Missなど）を管理するシングルトンクラス。
/// 判定の種類、タイミングの評価、スコアやコンボへの影響もここで処理する。
/// </summary>
public class JudgementManager : SingletonMonoBehaviour<JudgementManager>
{
    /// <summary>
    /// 判定の設定リスト（PerfectやMissなど。
    /// </summary>
    private List<JudgementConfig> judgementConfigs;

    /// <summary>
    /// 各判定の発生回数を記録する辞書。
    /// </summary>
    private Dictionary<string, int> judgementCounts = new();

    /// <summary>
    /// 判定が適用されたときに発火するイベント。
    /// </summary>
    internal event Action OnJudgementApplied;

    /// <summary>
    /// 判定設定を初期化する。
    /// </summary>
    /// <param name="configs">判定設定リスト</param>
    internal void Setup(List<JudgementConfig> configs)
    {
        judgementConfigs = new List<JudgementConfig>(configs);

        // "Miss" 判定が未登録の場合、フェイルセーフとして追加
        if (!judgementConfigs.Exists(j => j.Logic.SetJudgementName == "Miss"))
        {
            DebugManager.LogWarning("[JudgementManager] Miss 判定が登録されていません。フェイルセーフとして追加します。");
            judgementConfigs.Add(JudgementConfig.CreateFallbackMiss());
        }

        // 判定をタイミング差が小さい順（精度の高い順）にソート（Perfect → Miss）
        judgementConfigs.Sort((a, b) => a.Logic.SetMaxTimeDifference.CompareTo(b.Logic.SetMaxTimeDifference));
    }

    /// <summary>
    /// 判定カウントをすべてリセットする。
    /// </summary>
    internal void ResetAllJudgement()
    {
        judgementCounts.Clear();
    }

    /// <summary>
    /// 登録されているすべての判定設定を取得。
    /// </summary>
    internal List<JudgementConfig> GetAllJudgements() => judgementConfigs;

    /// <summary>
    /// "Miss" 判定を取得する。
    /// </summary>
    internal JudgementConfig GetMissJudgement()
    {
        var miss = judgementConfigs.FirstOrDefault(j => j.Logic.JudgementName == "Miss");
        if (miss == null)
        {
            DebugManager.LogError("[JudgementManager] Miss 判定が取得できませんでした。");
        }
        return miss;
    }

    /// <summary>
    /// 入力タイミングの差から該当する判定を評価する。
    /// </summary>
    /// <param name="timeDifference">ノーツと入力のタイミング差</param>
    /// <returns>該当する判定設定</returns>
    internal JudgementConfig EvaluateTiming(float timeDifference)
    {
        foreach (var judgement in judgementConfigs)
        {
            // 入力とノーツのタイミング差が、その判定の最大許容時間内であれば

            if (Mathf.Abs(timeDifference) <= judgement.Logic.SetMaxTimeDifference)
            {
                // この判定（例: Perfect）を返す
                return judgement;
            }
        }

        // どの判定にも該当しない場合は Miss とする
        return GetMissJudgement();
    }

    /// <summary>
    /// 最も遅いタイミング許容幅（＝Miss 判定の境界値）を取得。
    /// 入力受付のリミットとして使用される。
    /// </summary>
    internal float GetMaxJudgementTime()
    {
        //判定の設定リストに入ってない、もしくは何も要素がないなら
        if (judgementConfigs == null || !judgementConfigs.Any())
        {
            // エラー出して 処理しない
            DebugManager.LogError("[JudgementManager] 判定設定が存在しません。");
            return 0f;
        }
        // 判定リストの中で、最大の許容時間（SetMaxTimeDifference）を持つ値を返す
        return judgementConfigs.Max(j => j.Logic.SetMaxTimeDifference);
    }

    /// <summary>
    /// 判定を適用し、スコア・コンボ・エフェクト等の処理を行う。
    /// </summary>
    /// <param name="config">適用する判定</param>
    /// <param name="laneNumber">該当するレーン番号</param>
    internal void ApplyJudgement(JudgementConfig config, int laneNumber)
    {
        // 判定ごとのカウントを更新
        if (!judgementCounts.ContainsKey(config.Logic.JudgementName))
            judgementCounts[config.Logic.JudgementName] = 0;

        judgementCounts[config.Logic.JudgementName]++;

        // イベント通知
        OnJudgementApplied?.Invoke();

        DebugManager.Log(config.Logic.JudgementName.ToString());

        // スコアを加算
        ScoreManager.Instance.AddScore(config.Logic.SetScoreValue);

        // コンボ更新
        if (config.Logic.ShouldBreakCombo)
        {
            // コンボを切る
            ComboManager.Instance.ResetCombo();
        }
        else
        {
            // コンボを継続
            ComboManager.Instance.IncrementCombo();
            //コンボエフェクトを表示
            AnimationManager.Instance.ShowComboEffect(config);
        }
    }

    /// <summary>
    /// 指定した判定名の発生回数を取得。
    /// </summary>
    /// <param name="label">判定名（例："Perfect"）</param>
    /// <returns>発生回数（なければ0）</returns>
    internal int GetJudgementCount(string label)
    {
        return judgementCounts.TryGetValue(label, out int count) ? count : 0;
    }
}
