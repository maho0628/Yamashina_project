using UnityEngine;

/// <summary>
/// ゲージの見た目設定
/// </summary>
[System.Serializable]
public class GaugeVisualConfig
{
    #region ゲージの見た目設定の内部管理用変数

    /// <summary>
    /// ゲージの背景色
    /// </summary>
    [Header("▼ ゲージの基本色設定")]

    [SerializeField, Tooltip("ゲージの背景色")]
    private Color gaugeBackgroundColor = Color.gray;

    [Space(15)]

    /// <summary>
    /// ゲージの基本塗り色
    /// </summary>
    [SerializeField, Tooltip("ゲージの基本塗り色")]
    private Color gaugeFillColor = Color.cyan;

    #endregion


    #region 読み取り専用プロパティ(ゲージの見た目設定の内部管理用変数)

    /// <summary>
    /// ゲージの背景色の読み取り専用
    /// </summary>
    internal Color GaugeBackgroundColor => gaugeBackgroundColor;

    /// <summary>
    /// ゲージの基本塗り色の読み取り専用
    /// </summary>
    internal Color GaugeFillColor => gaugeFillColor;

    #endregion

}
