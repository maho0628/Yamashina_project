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

    [SerializeField, Header("各判定ごとのエフェクト")] 
    private GameObject hitEffect;


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
   
    internal GameObject HitEffect => hitEffect; 
    #region コンストラクタ
    /// <summary>
    /// デフォルトコンストラクタ
    /// </summary>
    public JudgementVisualConfig() { }

 
    #endregion
}