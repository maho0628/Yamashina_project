using UnityEngine;

/// <summary>
/// アニメーションのタイミング設定クラス
/// </summary>
[System.Serializable]
public class TextTimingSettings
{
    #region アニメーションのアニメーションのタイミング設定の内部管理用変数

    /// <summary>
    /// フェードインにかかる時間（秒単位）。
    /// </summary>
    [SerializeField, Tooltip("フェードインにかかる時間（秒単位）。")]
    private float fadeInDuration = 0.3f;

    [Space(15)]

    /// <summary>
    /// フェードイン後の最終アルファ値（0 = 完全に透明, 1 = 完全に不透明）。
    /// </summary>
    [SerializeField, Tooltip("フェードイン後の最終アルファ値\n（0 = 完全に透明, 1 = 完全に不透明）。")] 
    private float fadeInAlpha = 1f;

    [Space(15)]

    /// <summary>
    /// テキストが完全に表示された状態を保つ時間（秒単位）。
    /// </summary>
    [SerializeField, Tooltip("テキストが完全に表示された状態を保つ時間（秒単位）。")]
    private float displayDuration = 1.0f;

    [Space(15)]

    /// <summary>
    /// フェードアウトにかかる時間（秒単位）。
    /// </summary>
    [SerializeField, Tooltip("フェードアウトにかかる時間（秒単位）。")]
    private float fadeOutDuration = 0.3f;

    [Space(15)]

    /// <summary>
    /// フェードアウト後の最終アルファ値（0 = 完全に透明, 1 = 完全に不透明）。
    /// </summary>
    [Tooltip("フェードアウト後の最終アルファ値\n（0 = 完全に透明, 1 = 完全に不透明）。")]
    [SerializeField] private float fadeOutAlpha = 0f;

    #endregion


    #region  読み取り専用フィールド（アニメーションのアニメーションのタイミング設定の内部管理用変数)

    /// <summary>
    /// フェードインにかかる時間（秒単位）の読み取り専用
    /// </summary>
    internal float FadeInDuration => fadeInDuration;

    /// <summary>
    /// フェードイン後の最終アルファ値（0 = 完全に透明, 1 = 完全に不透明）の読み取り専用
    /// </summary>
    internal float FadeInAlpha => fadeInAlpha;

    /// <summary>
    /// テキストが完全に表示された状態を保つ時間（秒単位）の読み取り専用
    /// </summary>
    internal float DisplayDuration => displayDuration;

    /// <summary>
    /// フェードアウトにかかる時間（秒単位)の読み取り専用
    /// </summary>
    internal float FadeOutDuration => fadeOutDuration;

    /// <summary>
    ///  フェードアウト後の最終アルファ値（0 = 完全に透明, 1 = 完全に不透明）の読み取り専用
    /// </summary>
    internal float FadeOutAlpha => fadeOutAlpha;

    #endregion


}
