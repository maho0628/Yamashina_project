using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ノーツの判定関連の設定を保持するクラス。
/// 判定の種類ごとに許容時間や表示設定をまとめて管理します。
/// 選曲画面のUIイメージにも利用可能です。
/// StageConfigTableにて情報を使用
/// </summary>
public class JudgementConfigTable : ScriptableObject
{
    #region 判定のリストやディクショナリの内部管理用変数

    /// <summary>
    /// 判定一つ分の設定情報のデータをまとめたリスト
    /// </summary>
    [Header("▼ゲーム内で使用する判定関連の一覧")]
    [SerializeField, Tooltip(" 判定一つ分の設定情報のデータをまとめたリスト")]
    private List<JudgementConfig> judgementLists = new List<JudgementConfig>();


    /// <summary>
    /// ゲーム内で使用する判定関連のディクショナリ
    /// </summary>
    private Dictionary<string, JudgementConfig> judgementConfigDict;

    #endregion


    #region Unity イベント

    private void OnEnable()
    {
        // ScriptableObject 再読み込み時にも対応
        InitializeDictionary();
    }

    private void OnValidate()
    {
        // Inspector での変更時にディクショナリを再構築
        if (Application.isPlaying)
        {
            InitializeDictionary();
        }
    }

    #endregion


    #region プライベートメソッド

    /// <summary>
    /// ディクショナリの初期化
    /// </summary>
    private void InitializeDictionary()
    {
        judgementConfigDict = new Dictionary<string, JudgementConfig>();

        if (judgementLists == null || judgementLists.Count == 0)
        {
            DebugManager.LogWarning($"[JudgementConfigTable] 判定リストが空です: {name}");
            return;
        }

        foreach (var judgement in judgementLists)
        {
            if (judgement == null)
            {
                DebugManager.LogWarning($"[JudgementConfigTable] nullの判定設定が含まれています: {name}");
                continue;
            }

            if (judgement.Logic == null)
            {
                DebugManager.LogWarning($"[JudgementConfigTable] LogicConfigがnullの判定設定があります: {name}");
                continue;
            }

            string judgementName = judgement.Logic.JudgementName;

            // 判定関連の一覧のリストのJudgementNameに文字列が入ってる＆ディクショナリにその文字列（キー）が含まれていないなら
            if (!string.IsNullOrEmpty(judgementName) && !judgementConfigDict.ContainsKey(judgementName))
            {
                // ディクショナリにその文字列を追加
                judgementConfigDict.Add(judgementName, judgement);
                DebugManager.Log($"[JudgementConfigTable] 登録されたJudgementName: {judgementName}");
            }
            else
            {
                // 同じキーを登録しようとしているかJudgementNameが空白
                if (string.IsNullOrEmpty(judgementName))
                {
                    DebugManager.LogWarning($"[JudgementConfigTable] JudgementNameが空または null です: {name}");
                }
                else
                {
                    DebugManager.LogWarning($"[JudgementConfigTable] 重複したJudgementName: {judgementName} in {name}");
                }
            }
        }

        DebugManager.Log($"[JudgementConfigTable] 初期化完了。登録数: {judgementConfigDict.Count}");
    }

    #endregion

}



