using TMPro;
using UnityEngine;

/// <summary>
/// アニメーションの基本設定のクラス
/// </summary>
[System.Serializable]
public class TextBasicSettings
{
    #region  アニメーションの基本設定の内部管理用変数

    /// <summary>
    /// 表示するアニメーションテキスト
    /// </summary>
    [SerializeField, Tooltip("演出で表示する文字列。例：\"Ready\", \"Start\", \"Game Over\"など")]
    private string animationText = "Ready";

    /// <summary>
    /// アニメーションの文字色
    /// </summary>
    [SerializeField, Tooltip("アニメーションの文字色")]
    private Color textColor = Color.white;

    /// <summary>
    /// アニメーションの文字の大きさ
    /// </summary>
    [SerializeField, Tooltip("アニメーションの文字の大きさ")]
    private int fontSize = 48;

    /// <summary>
    /// 使用するアニメーションの種類
    /// </summary>
    [SerializeField, Tooltip("テキストの表示アニメーションの種類を選択します")]
    private AnimationType animationType = AnimationType.Simple;

    /// <summary>
    /// 使用するフォントアセット
    /// </summary>
    [SerializeField, Tooltip("アニメーションテキストに使用するTMPフォントアセット")]
    private TMP_FontAsset fontAsset;

    /// <summary>
    /// フォントスタイル（太字、斜体など）
    /// </summary>
    [SerializeField, Tooltip("フォントのスタイル（Bold, Italicなど）を指定します")]
    private FontStyles animationFontStyles;

    #endregion

    #region 読み取り専用フィールド(アニメーションの基本設定の内部管理用変数)

    /// <summary>
    /// 表示するアニメーションテキスト の読み取り専用 
    /// </summary>
    internal string AnimationText
    {
        get { return animationText; }
        set { animationText = value; }
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
    /// 使用するアニメーションの種類の読み取り専用
    /// </summary>
    internal AnimationType AnimationType
    {
        get { return animationType; }
        set { animationType = value; }
    }

    /// <summary>
    /// 使用するフォントアセットの読み取り専用
    /// </summary>
    internal TMP_FontAsset FontAsset => fontAsset;

    /// <summary>
    /// フォントスタイル（太字、斜体など）の読み取り専用
    /// </summary>
    internal FontStyles AnimationFontStyles => animationFontStyles;

    #endregion
}
