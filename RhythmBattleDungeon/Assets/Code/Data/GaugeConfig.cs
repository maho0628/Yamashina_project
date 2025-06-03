using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// スコアゲージに関するスクリプタブルオブジェクト
/// </summary>
[CreateAssetMenu(fileName = "GaugeConfig", menuName = "GameConfig/GaugeConfig")]
public class GaugeConfig : ScriptableObject
{
    #region  スコアゲージ関連のアニメーション設定に関する内部管理用変数


    [Header("■ アニメーション設定")]
    [SerializeField, Tooltip("ゲージ補間時間（秒）")]
    private float gaugeLerpDuration = 0.5f;

    [SerializeField, Tooltip("補間にEasingを使うか")]
    private bool useEasing = false;

    [SerializeField, Tooltip("ゲージアニメーションに使う補間カーブ（イージング）")]
    private AnimationCurve gaugeAnimationCurve = AnimationCurve.Linear(0, 0, 1, 1);

    [SerializeField, Tooltip("スコア上昇時の演出カラー")]
    private Color scoreGainColor = Color.green;

    [SerializeField, Tooltip("MAX時にゲージをフラッシュさせるか")]
    private bool flashOnFull = false;

    [SerializeField, Tooltip("MAX時の演出エフェクト")]
    private GameObject flashEffectPrefab;

    #endregion


    #region スコアゲージ関連のUIの見た目設定に関する内部管理用変数

    [Header("■ UIの見た目設定")]
    [SerializeField, Tooltip("ゲージの背景色")]
    private Color gaugeBackgroundColor = Color.gray;

    [SerializeField, Tooltip("ゲージの基本塗り色")]
    private Color gaugeFillColor = Color.cyan;

    [SerializeField, Tooltip("ゲージの割合に応じて色を変える")]
    private List<ThresholdColor> thresholdColors;

    #endregion


    #region スコアゲージ関連のデバッグ設定に関する内部管理用変数

    [Header("■ デバッグ設定")]
    [SerializeField, Tooltip("スコア変化しなくても常にゲージをアニメさせる")]
    private bool debugAlwaysAnimate = false;

    [SerializeField, Tooltip("初期ゲージ値（0〜1）")]
    [Range(0f, 1f)]
    private float debugInitialValue = 0f;

    #endregion


    #region  読み取り専用プロパティ (スコアゲージ関連のアニメーション設定に関する内部管理用変数)
    /// <summary>
    /// k
    /// </summary>
    internal float GaugeLerpDuration { get { return gaugeLerpDuration; } }

    internal bool UseEasing { get { return useEasing; } }

    internal AnimationCurve GaugeAnimationCurve {  get { return gaugeAnimationCurve; } }    

    internal Color ScoreGainColor { get { return scoreGainColor; } }    

    internal bool FlashOnFull { get { return flashOnFull; } }   

    internal GameObject FlashEffectPrefab {  get { return flashEffectPrefab; } }

    #endregion

    #region  読み取り専用プロパティ (スコアゲージ関連のUIの見た目設定に関する内部管理用変数)


    internal Color GaugeBackgroundColor {  get { return gaugeBackgroundColor; } }   
    internal Color GaugeFillColor { get {   return gaugeFillColor; } }

    internal List<ThresholdColor> ThresholdColors {  get { return thresholdColors; } }

    #endregion


    #region 読み取り専用プロパティ(スコアゲージ関連のデバッグ設定に関する内部管理用変数)


    internal bool DebugAlwaysAnimate { get { return debugAlwaysAnimate; } } 

    internal float DebugInitialValue { get { return debugInitialValue; } }  


    #endregion
}

