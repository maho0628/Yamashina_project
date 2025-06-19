using UnityEngine;

/// <summary>
/// 判定のロジックに関するクラス
/// </summary>
[System.Serializable]
public class JudgementLogicConfig
{
    #region ロジック判定設定に関する内部情報処理変数

    /// <summary>
    /// 判定の名前（内部識別用）
    /// </summary>
    [SerializeField, Tooltip(" 判定の名前（内部識別用）")]
    private string judgementName;

    [Space(15)]

    /// <summary>
    /// 許容タイミング（±秒）
    /// </summary>
    [SerializeField, Tooltip("許容タイミング（±秒）")]
    private float maxTimeDifference = 0.05f;

    [Space(15)]

    /// <summary>
    /// 加算スコア
    /// </summary>
    [SerializeField, Tooltip("加算スコア")]
    private int scoreValue = 100;

    [Space(15)]

    /// <summary>
    /// この判定でコンボを切るか？
    /// </summary>
    [SerializeField, Tooltip("この判定でコンボを切るか？")]
    private bool shouldBreakCombo = false;

    #endregion


    #region 読み取り専用プロパティ(ロジック判定設定に関する内部情報処理変数)

    /// <summary>
    /// 判定名の読み取り専用
    /// </summary>
    internal string JudgementName => judgementName;

    /// <summary>
    /// この判定でコンボを切るかの読み取り専用
    /// </summary>
    internal bool ShouldBreakCombo => shouldBreakCombo;


    #endregion


    #region セッターメソッド（ロジック判定設定に関する内部情報処理変数)

    /// <summary>
    /// 判定名の設定用プロパティ
    /// </summary>
    internal string SetJudgementName { get => judgementName; set => judgementName = value; }

    /// <summary>
    /// 許容時間の設定用プロパティ
    /// </summary>
    internal float SetMaxTimeDifference { get => maxTimeDifference; set => maxTimeDifference = value; }

    /// <summary>
    /// スコア値の設定用プロパティ
    /// </summary>
    internal int SetScoreValue { get => scoreValue; set => scoreValue = value; }

    /// <summary>
    /// コンボを切るかどうかの設定用プロパティ
    /// </summary>
    internal bool SetShouldBreakCombo { get => shouldBreakCombo; set => shouldBreakCombo = value; }

    #endregion


    #region コンストラクタ

    /// <summary>
    /// デフォルトコンストラクタ
    /// </summary>
    internal JudgementLogicConfig() { }

    #endregion
}