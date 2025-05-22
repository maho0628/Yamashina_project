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
    #region リストやディクショナリ変数

    /// <summary>
    ///ゲーム内で使用する判定関連の一覧のリスト
    /// </summary>
    [SerializeField, Header("ゲーム内で使用する判定関連の一覧")]
    private List<JudgementConfig> judgementLists = new List<JudgementConfig>();

    /// <summary>
    /// ゲーム内で使用する判定関連のディクショナリ
    /// </summary>
    private Dictionary<string, JudgementConfig> judgementConfigDict;

    #endregion


    #region 読み取り専用プロパティ

    /// <summary>
    /// 判定関連のリストの読み取り専用
    /// </summary>
    internal List<JudgementConfig> JudgementLists => judgementLists;

    #endregion


    #region ゲッターメソッド

    /// <summary>
    ///判定関連のリスト情報をすべて返す 
    /// </summary>
    /// <returns>JudgementConfigのList</returns>
    internal List<JudgementConfig> GetAllJudgementConfig()
    {
        return judgementLists;
    }

    /// <summary>
    /// リスト内のJudgementConfigをIDで探して返す
    /// </summary>
    /// <param name="id"></param>
    /// <returns>JudgementConfig</returns>
    internal JudgementConfig GetJudgementConfig(string id)
    {
        if (judgementConfigDict == null)
        {
            InitializeDictionary();
        }

        judgementConfigDict.TryGetValue(id, out var config);
        return config;
    }

    #endregion



    private void OnEnable()
    {
        // ScriptableObject 再読み込み時にも対応
        InitializeDictionary();
    }


    #region プライベートメソッド

    /// <summary>
    /// ディクショナリの初期化
    /// </summary>
    private void InitializeDictionary()
    {
        judgementConfigDict = new Dictionary<string, JudgementConfig>();
        foreach (var judgement in judgementLists)
        {
            //判定関連の一覧のリストのJudgementNameに文字列が入ってる＆ディクショナリにその文字列（キー）が含まれていないなら
            if (!string.IsNullOrEmpty(judgement.JudgementName) && !judgementConfigDict.ContainsKey(judgement.JudgementName))
            {
                // ディクショナリにその文字列を追加
                judgementConfigDict.Add(judgement.JudgementName, judgement);
                foreach (var key in judgementConfigDict.Keys)
                {
                    //どのキーが登録されているかのデバッグログ
                    Debug.Log($"登録されているJudgementName: {key}");
                }
            }
            else
            {
                //同じキーを登録しようとしているかJudgementNameが空白
                Debug.LogWarning($"[JudgementConfigTable] 重複または空のBGM ID: {judgement.JudgementName}");
            }
        }
    }

    #endregion

}

