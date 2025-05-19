using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ノーツの判定関連の設定を保持するスクリプタブルオブジェクト。
/// 判定の種類ごとに許容時間や表示設定をまとめて管理します。
/// 選曲画面のUIイメージにも利用可能です。
/// </summary>
[CreateAssetMenu(menuName = "RhythmGame/JudgementConfig")]
public class JudgementConfigTable : ScriptableObject
{
    /// <summary>
    ///ゲーム内で使用する判定関連の一覧のリスト
    /// </summary>
    [SerializeField]
    private List<JudgementConfig> judgementList;


    // === 読み取り専用プロパティ ===

    /// <summary>
    /// 判定関連のリストの読み取り専用
    /// </summary>
    internal List<JudgementConfig> Judgements => judgementList;

}

