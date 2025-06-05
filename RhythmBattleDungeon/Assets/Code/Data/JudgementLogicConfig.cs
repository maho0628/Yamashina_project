using UnityEngine;

/// <summary>
/// 判定のロジックに関するクラス
/// </summary>
[System.Serializable]
public class JudgementLogicConfig
{
    #region フィールド
    [SerializeField, Header("判定の名前（内部識別用）")]
    private string judgementName;

    [SerializeField, Header("許容タイミング（±秒）")]
    private float maxTimeDifference = 0.05f;

    [SerializeField, Header("加算スコア")]
    private int scoreValue = 100;

    [SerializeField, Header("この判定でコンボを切るか？")]
    private bool shouldBreakCombo = false;
    #endregion

    #region 読み取り専用プロパティ
    /// <summary>
    /// 判定名の読み取り専用
    /// </summary>
    internal string JudgementName => judgementName;

    /// <summary>
    /// 判定の許容時間の読み取り専用
    /// </summary>
    internal float MaxTimeDifference => maxTimeDifference;

    /// <summary>
    /// この判定で加算されるスコア値の読み取り専用
    /// </summary>
    internal int ScoreValue => scoreValue;


    #endregion

    #region 設定用プロパティ（必要に応じて）
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
    internal bool ShouldBreakCombo { get => shouldBreakCombo; set => shouldBreakCombo = value; }
    #endregion

    #region コンストラクタ
    /// <summary>
    /// デフォルトコンストラクタ
    /// </summary>
    public JudgementLogicConfig() { }

    /// <summary>
    /// 基本初期化用コンストラクタ
    /// </summary>
    /// <param name="name">判定名</param>
    /// <param name="maxDiff">許容タイミング（±秒）</param>
    internal JudgementLogicConfig(string name, float maxDiff)
    {
        judgementName = name;
        maxTimeDifference = maxDiff;
    }

    /// <summary>
    /// スコア付き初期化用コンストラクタ
    /// </summary>
    /// <param name="name">判定名</param>
    /// <param name="maxDiff">許容タイミング（±秒）</param>
    /// <param name="score">加算スコア</param>
    internal JudgementLogicConfig(string name, float maxDiff, int score)
    {
        judgementName = name;
        maxTimeDifference = maxDiff;
        scoreValue = score;
    }


    /// <summary>
    /// SetLogicメソッド用のコンストラクタ（デフォルト値付き）
    /// </summary>
    /// <param name="name">判定名</param>
    /// <param name="maxDiff">許容タイミング（±秒、デフォルト: 0.05f）</param>
    /// <param name="score">加算スコア（デフォルト: 100）</param>
    /// <param name="breakCombo">この判定でコンボを切るか（デフォルト: false）</param>
    internal JudgementLogicConfig(string name, float maxDiff = 0.05f, int score = 100, bool breakCombo = false)
    {
        judgementName = name;
        maxTimeDifference = maxDiff;
        scoreValue = score;
        shouldBreakCombo = breakCombo;
    }
    #endregion
}