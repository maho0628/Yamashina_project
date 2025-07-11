using DG.Tweening;
using UnityEngine;

/// <summary>
/// 判定の見た目のクラス
/// </summary>
[System.Serializable]
public class JudgementVisualConfig
{
    #region 判定表示設定の内部情報処理変数

    /// <summary>
    /// 画面に表示される判定名
    /// </summary>
    [Header("▼判定表示設定")]

    [SerializeField, Tooltip("画面に表示される判定名")]
    private string displayName = "PERFECT";

    [Space(15)]

    /// <summary>
    /// 表示カラー
    /// </summary>
    [SerializeField, Tooltip("表示カラー")]
    private Color displayColor = Color.white;

    [Space(15)]

    /// <summary>
    /// 表示時間（秒）
    /// </summary>
    [SerializeField, Tooltip("表示時間（秒）")]
    private float showDuration = 0.5f;

    [Space(15)]

    /// <summary>
    /// フェードアウト時間（秒）
    /// </summary>
    [SerializeField, Tooltip("フェードアウト時間（秒）")]
    private float fadeOutDuration = 0.3f;

    [Space(15)]

    #endregion


    #region スケーリングの内部情報処理変数

    /// <summary>
    /// スケールイン時間
    /// </summary>
    [Header("▼スケーリング")]

    [SerializeField, Tooltip("スケールイン時間")]
    private float scaleInTime = 0.2f;

    [Space(15)]

    /// <summary>
    /// イージングタイプ
    /// </summary>
    [SerializeField, Tooltip("イージングタイプ")]
    private Ease scaleEase = Ease.OutBack;

    [Space(15)]

    #endregion


    #region 演出設定の内部情報処理変数

    /// <summary>
    /// コンボ演出用の視覚効果設定
    /// </summary>
    [Header("▼コンボ演出設定")]

    [SerializeField, Tooltip("コンボ演出用の視覚効果設定")]
    private ComboEffectConfig comboEffectConfig = new ComboEffectConfig();

    [Space(15)] 

    /// <summary>
    /// 判定演出用の視覚効果設定
    /// </summary>
    [Header("▼判定演出設定")]

    [SerializeField, Tooltip("判定演出用の視覚効果設定")]
    private JudgementEffectConfig judgementConfig = new JudgementEffectConfig();

    [Space(15)]

    /// <summary>
    /// スコア演出用の視覚効果設定
    /// </summary>
    [Header("▼スコア演出設定")]

    [SerializeField, Tooltip("スコア演出用の視覚効果設定")]
    private ScoreEffectConfig scoreEffectConfig = new ScoreEffectConfig();

    #endregion

    
    #region 読み取り専用プロパティ(判定表示設定の内部情報処理変数)

    /// <summary>
    /// プレイ中に表示される判定の表示名の読み取り専用
    /// </summary>
    internal string DisplayJudgementName => displayName;

    /// <summary>
    /// 判定表示用カラーの読み取り専用
    /// </summary>
    internal Color DisplayColor => displayColor;


    /// <summary>
    /// 表示時間の読み取り専用
    /// </summary>
    internal float ShowDuration => showDuration;

    /// <summary>
    /// フェードアウト時間の読み取り専用
    /// </summary>
    internal float FadeOutDuration => fadeOutDuration;

    #endregion


    #region 読み取り専用プロパティ(スケーリングの内部情報処理変数)

    /// <summary>
    /// スケールイン時間の読み取り専用
    /// </summary>
    internal float ScaleInTime => scaleInTime;

    /// <summary>
    /// イージングタイプの読み取り専用
    /// </summary>
    internal Ease SetScaleEase => scaleEase;

    #endregion


    #region 読み取り専用プロパティ( 演出設定の内部情報処理変数)

    /// <summary>
    /// コンボ演出用の設定の読み取り専用
    /// </summary>
    internal ComboEffectConfig ComboEffect => comboEffectConfig;

    /// <summary>
    /// 判定演出用の設定の読み取り専用
    /// </summary>
    internal JudgementEffectConfig JudgementEffect => judgementConfig;

    /// <summary>
    /// スコア演出用の設定の読み取り専用
    /// </summary>
    internal ScoreEffectConfig ScoreEffect => scoreEffectConfig;

    #endregion


    #region コンストラクタ

    /// <summary>
    /// デフォルトコンストラクタ
    /// </summary>
    internal JudgementVisualConfig() { }

    #endregion
}