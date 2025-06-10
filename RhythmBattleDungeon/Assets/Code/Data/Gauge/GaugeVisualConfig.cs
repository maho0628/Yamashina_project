using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// ゲージの見た目設定
/// </summary>
[System.Serializable]
public class GaugeVisualConfig
{
    #region ゲージの見た目設定に関する内部管理用変数

    /// <summary>
    /// ゲージの背景色
    /// </summary>
    [Header("ゲージの色設定")]
    [SerializeField, Tooltip("ゲージの背景色")]
    private Color gaugeBackgroundColor = Color.gray;

    /// <summary>
    /// ゲージの基本塗り色
    /// </summary>
    [SerializeField, Tooltip("ゲージの基本塗り色")]
    private Color gaugeFillColor = Color.cyan;

    /// <summary>
    /// ゲージの割合に応じて色を変える設定
    /// </summary>
    [Space(10)]
    [SerializeField, Tooltip("ゲージの割合に応じて色を変える設定")]
    private List<ThresholdColor> thresholdColors;


    #endregion

    #region 読み取り専用プロパティ(ゲージの見た目設定に関する内部管理用変数)

    /// <summary>
    /// ゲージの背景色の読み取り専用
    /// </summary>
    internal Color GaugeBackgroundColor => gaugeBackgroundColor;

    /// <summary>
    /// ゲージの基本塗り色の読み取り専用
    /// </summary>
    internal Color GaugeFillColor => gaugeFillColor;

    /// <summary>
    /// ゲージの割合に応じて色を変える設定の読み取り専用
    /// </summary>
    internal List<ThresholdColor> ThresholdColors => thresholdColors;

    #endregion

  

    /// <summary>
    /// /// ゲージの残量に応じて色を変える設定用クラス。
    /// </summary>
    [System.Serializable]
    internal class ThresholdColor
    {
        #region ゲージの残量に応じて色を変更するための内部管理用変数

        /// <summary>
        /// この割合以下になったときに適用される色の基準値（0～1）。
        /// 例: threshold = 0.3 の場合、ゲージが30%以下になると gaugeColor が使われます。
        /// </summary>
        [Header("残量が一定以下になったときの色設定"), Range(0f, 1f)]
        [SerializeField, Tooltip("ゲージの割合がこの値以下になったときに、この色が適用されます。\n0〜1の範囲で設定します（例：0.3 → 30%以下）。")]
        private float minRatioForThisColor;


        /// <summary>
        /// ゲージが指定された割合以下になったときに表示される色。
        /// </summary>
        [SerializeField, Tooltip("上の割合以下になったときに使うゲージの色です。")]
        private Color colorWhenBelow;

        #endregion


        #region 読み取り専用プロパティ(ゲージの残量に応じて色を変更するための内部管理用変数)

        /// <summary>
        // この割合以下になったときに色を変更するための基準値（0〜1）の読み取り専用
        /// 例：0.3 → ゲージ残量が30%以下になると指定した色が適用されます。
        /// </summary>
        internal float MinRatioForThisColor => minRatioForThisColor;

        /// <summary>
        /// ゲージが指定された割合以下になったときに表示される色の読み取り専用
        /// </summary>
        internal Color ColorWhenBelow => colorWhenBelow;

        #endregion

    }

}
