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
            //リスト内の判定情報がないなら
            if (judgement == null)
            {
                //ワーニングを出して続行
                DebugManager.LogWarning($"[JudgementConfigTable] nullの判定設定が含まれています: {name}");
                continue;
            }

            //判定情報内のロジック設定を取得して、データが入ってこないなら
            var logic = judgement.Logic;    
            if (logic == null)
            {
                //ワーニングを出して続行
                DebugManager.LogWarning($"[JudgementConfigTable] LogicConfigがnullの判定設定があります: {name}");
                continue;
            }

            string judgementName = logic.JudgementName;

            // 判定関連の一覧のリストのJudgementNameに文字列が入ってる＆ディクショナリにその文字列（キー）が含まれていないなら
            if (!string.IsNullOrEmpty(judgementName) && !judgementConfigDict.ContainsKey(judgementName))
            {
                // ディクショナリにその文字列を追加
                judgementConfigDict.Add(judgementName, judgement);
                DebugManager.Log($"[JudgementConfigTable] 登録されたJudgementName: {judgementName}");
            }
            else
            {
                // JudgementNameが空白なら
                if (string.IsNullOrEmpty(judgementName))
                {
                    //JudgementNameが空または nullなのでワーニングを出す
                    DebugManager.LogWarning($"[JudgementConfigTable] JudgementNameが空または null です: {name}");
                }
                else
                {
                    //同じキーを登録しようとしているのでワーニングを出す
                    DebugManager.LogWarning($"[JudgementConfigTable] 重複したJudgementName: {judgementName} in {name}");
                }
            }
        }
        //JudgementConfigTable] 初期化完了し、judgementConfigDict.Countのログを表示
        DebugManager.Log($"[JudgementConfigTable] 初期化完了。登録数: {judgementConfigDict.Count}");
    }

    #endregion

}



