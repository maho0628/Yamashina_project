using DG.Tweening;
using UnityEngine;

/// <summary>
/// 判定の見た目に関するクラス
/// </summary>
[System.Serializable]
public class JudgementVisualConfig
{
    #region フィールド
    [SerializeField, Header("画面に表示される判定名")]
    private string displayName = "PERFECT";

    [SerializeField, Header("表示カラー")]
    private Color displayColor = Color.white;

    [SerializeField, Header("表示アイコン（任意）")]
    private Sprite displayIcon;

    [SerializeField, Header("表示時間（秒）")]
    private float showDuration = 0.5f;

    [SerializeField, Header("フェードアウト時間（秒）")]
    private float fadeOutDuration = 0.3f;
    #endregion
    [Header("スケーリング")]
    [SerializeField] private float scaleInTime = 0.2f;
    [SerializeField] private Ease scaleEase = Ease.OutBack;



    #region 読み取り専用プロパティ
    /// <summary>
    /// プレイ中に表示される判定の表示名の読み取り専用
    /// </summary>
    internal string DisplayJudgementName => displayName;

    /// <summary>
    /// 判定表示用カラーの読み取り専用
    /// </summary>
    internal Color DisplayColor => displayColor;

    /// <summary>
    /// 判定表示用アイコンの読み取り専用
    /// </summary>
    internal Sprite DisplayIcon => displayIcon;

    /// <summary>
    /// 表示時間の読み取り専用
    /// </summary>
    internal float ShowDuration => showDuration;

    /// <summary>
    /// フェードアウト時間の読み取り専用
    /// </summary>
    internal float FadeOutDuration => fadeOutDuration;
    #endregion

    internal float SetScaleInTime => scaleInTime;
    internal Ease SetScaleEase => scaleEase;
    #region 設定用プロパティ（必要に応じて）
    /// <summary>
    /// 表示名の設定用プロパティ
    /// </summary>
    internal string SetDisplayJudgementName { get => displayName; set => displayName = value; }

    /// <summary>
    /// 表示色の設定用プロパティ
    /// </summary>
    internal Color SetDisplayColor { get => displayColor; set => displayColor = value; }

    /// <summary>
    /// 表示アイコンの設定用プロパティ
    /// </summary>
    internal Sprite SetDisplayIcon { get => displayIcon; set => displayIcon = value; }

    /// <summary>
    /// 表示時間の設定用プロパティ
    /// </summary>
    internal float SetShowDuration { get => showDuration; set => showDuration = value; }

    /// <summary>
    /// フェードアウト時間の設定用プロパティ
    /// </summary>
    internal float SetFadeOutDuration { get => fadeOutDuration; set => fadeOutDuration = value; }
    #endregion

    #region コンストラクタ
    /// <summary>
    /// デフォルトコンストラクタ
    /// </summary>
    public JudgementVisualConfig() { }

    /// <summary>
    /// 基本初期化用コンストラクタ
    /// </summary>
    /// <param name="color">表示色</param>
    /// <param name="icon">表示アイコン（任意）</param>
    internal JudgementVisualConfig(Color color, Sprite icon = null)
    {
        displayColor = color;
        displayIcon = icon;
    }

    /// <summary>
    /// 表示名付き初期化用コンストラクタ
    /// </summary>
    /// <param name="name">表示名</param>
    /// <param name="color">表示色</param>
    /// <param name="icon">表示アイコン（任意）</param>
    internal JudgementVisualConfig(string name, Color color, Sprite icon = null)
    {
        displayName = name;
        displayColor = color;
        displayIcon = icon;
    }

    /// <summary>
    /// 完全初期化用コンストラクタ
    /// </summary>
    /// <param name="name">表示名</param>
    /// <param name="color">表示色</param>
    /// <param name="icon">表示アイコン（任意）</param>
    /// <param name="showTime">表示時間</param>
    /// <param name="fadeTime">フェードアウト時間</param>
    internal JudgementVisualConfig(string name, Color color, Sprite icon, float showTime, float fadeTime)
    {
        displayName = name;
        displayColor = color;
        displayIcon = icon;
        showDuration = showTime;
        fadeOutDuration = fadeTime;
    }
    #endregion
}