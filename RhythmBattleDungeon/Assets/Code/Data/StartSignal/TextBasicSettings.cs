using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
///テキストの基本設定のクラス
/// </summary>
[System.Serializable]
public class TextBasicSettings
{
    #region  テキストの基本設定の内部管理用変数

    /// <summary>
    /// 表示するテキスト
    /// </summary>
    [SerializeField, Tooltip("表示する文字列。\n例：\"Ready\", \"Start\", \"Game Over\"など")]
    private string displayText = "Ready";

    [Space(15)]

    /// <summary>
    /// 文字の色
    /// </summary>
    [SerializeField, Tooltip("文字の色")]
    private Color textColor = Color.white;

    [Space(15)]

    /// <summary>
    /// 文字の大きさ
    /// </summary>
    [SerializeField, Tooltip("文字の大きさ")]
    private int fontSize = 48;


    [Space(15)]

    /// <summary>
    /// 使用するフォントアセット
    /// </summary>
    [SerializeField, Tooltip("テキストに使用するTMPフォントアセット")]
    private TMP_FontAsset fontAsset;

    [Space(15)]

    /// <summary>
    /// フォントスタイル（Bold, Italicなど）
    /// </summary>
    [SerializeField, Tooltip("フォントのスタイル（Bold, Italicなど）を指定します")]
    private FontStyles displayFontStyles;

    #endregion


    #region 読み取り専用フィールド(アニメーションの基本設定の内部管理用変数)

    /// <summary>
    /// 表示するアニメーションテキスト の読み取り専用 
    /// </summary>
    internal string DisplayText
    {
        get { return displayText; }
        set { displayText = value; }
    }

    /// <summary>
    /// アニメーションの文字色の読み取り専用
    /// </summary>
    internal Color TextColor
    {
        get { return textColor; }
        set { textColor = value; }
    }

    /// <summary>
    ///アニメーションの文字の大きさの読み取り専用
    /// </summary>
    internal int FontSize => fontSize;

    /// <summary>
    /// 使用するフォントアセットの読み取り専用
    /// </summary>
    internal TMP_FontAsset FontAsset => fontAsset;

    /// <summary>
    /// フォントスタイル（太字、斜体など）の読み取り専用
    /// </summary>
    internal FontStyles DisplayFontStyles => displayFontStyles;

    #endregion

}
