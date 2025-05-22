using UnityEngine;

/// <summary>
/// 判定一つ分の設定情報のデータ
/// 判定名、許容タイミング、表示色、表示用アイコンを管理します。
/// </summary>
[System.Serializable]
public class JudgementConfig
{
    #region 判定の基本情報変数

    /// <summary>
    /// 判定の名前（例: Perfect, Great, Missなど）
    /// </summary>
    [SerializeField, Header("判定の名前（例: Perfect / Great / Miss など）")]
    private string judgementName;

    /// <summary>
    /// 判定が成立する許容時間（理想タイミングから±何秒以内か）
    /// 例：0.05秒なら理想タイミングの前後0.05秒以内でこの判定になる。
    /// </summary>
    [Tooltip("この秒数以内に押せばこの判定になります（例: 0.05 = ±0.05秒）")]
    [SerializeField, Header("判定の許容時間（理想タイミングから何秒ズレてもOKか）")]
    private float maxTimeDifference;

    #endregion


    #region 判定の見た目の設定変数

    /// <summary>
    /// 判定の表示に使うカラー。UIの色分けなどに利用。
    /// </summary>
    [SerializeField, Header("判定の表示カラー（UIなどに使用）")]
    private Color displayColor;

    /// <summary>
    /// 判定のアイコン画像（任意）
    /// 設定がない場合は名前のみ表示。
    /// </summary>
    [Tooltip("表示用の画像。設定しない場合は名前だけ表示されます。")]
    [SerializeField, Header("判定のアイコン（任意）")]
    private Sprite displayIcon;

    #endregion

    #region スコア・コンボ関連の情報変数

    /// <summary>
    /// 判定時に加算するスコア
    /// </summary>
    [SerializeField, Header("この判定時に加算するスコア")]
    private int scoreValue;

    /// <summary>
    /// この判定でコンボを切るかどうか、例：ミスならコンボを切るなど
    /// </summary>
    [SerializeField, Header("この判定でコンボを切るか、例：ミスならコンボを切るなど")]
    private bool breaksCombo;

    #endregion


    #region 読み取り専用プロパティ(判定の基本情報変数)

    /// <summary>
    /// 判定名の読み取り専用
    /// </summary>
    internal string JudgementName => judgementName;

    /// <summary>
    /// 判定の許容時間の読み取り専用
    /// </summary>
    internal float MaxTimeDifference => maxTimeDifference;
    #endregion


    #region 読み取り専用プロパティ(判定の見た目の設定変数)

    /// <summary>
    /// 判定表示用カラーの読み取り専用
    /// </summary>
    internal Color DisplayColor => displayColor;

    /// <summary>
    /// 判定表示用アイコンの読み取り専用
    /// </summary>
    internal Sprite DisplayIcon => displayIcon;
    #endregion


    #region 読み取り専用プロパティ(コンボ関連の情報変数)

    /// <summary>
    /// この判定で加算されるスコア値の読み取り専用
    /// </summary>
    internal int ScoreValue => scoreValue;

    /// <summary>
    /// この判定でコンボが切れるかどうかの読み取り専用
    /// </summary>
    internal bool BreaksCombo => breaksCombo;

    #endregion


    #region コンストラクタなど

    /// <summary>
    /// Fallback Miss を生成するための static factory
    /// </summary>
    /// <returns>JudgementConfig</returns>
    public static JudgementConfig CreateFallbackMiss()
    {
        return new JudgementConfig
        {
            judgementName = "Miss",
            maxTimeDifference = 999f,
            displayColor = Color.gray,
            displayIcon = null,
            scoreValue = 0,
            breaksCombo = false


        };
    }

    /// <summary>
    /// フォールバック用コンストラクタ
    /// </summary>
    /// <param name="name">判定名</param>
    /// <param name="maxDiff"> 判定が成立する許容時間</param>
    /// <param name="col">判定の表示に使うカラー</param>
    /// <param name="icon">判定のアイコン画像</param>
    /// <param name="score">スコアの値</param>
    /// <param name="breakCom">コンボが途切れるかどうか</param>
    /// <param name="breakCom">コンボが途切れるかどうか</param>
    public JudgementConfig(string name, float maxDiff, bool breakCom, int score, Color col, Sprite icon = null)
    {
        judgementName = name;
        maxTimeDifference = maxDiff;
        displayColor = col;
        displayIcon = icon;
        scoreValue = score;
        breaksCombo = breakCom;
    }

    /// <summary>
    /// JudgementConfigをNewする用
    /// </summary>
    public JudgementConfig() { }

    #endregion
}