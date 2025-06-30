using UnityEngine;

/// <summary>
/// アニメーションのスケール設定クラス
/// </summary>
[System.Serializable]
public class TextScaleSettings
{
    #region 　アニメーションのスケール設定の内部管理用変数

    /// <summary>
    /// アニメーション開始時のスケール。通常は 0〜1 の範囲で指定します。
    /// </summary>
    [SerializeField, Tooltip("アニメーション開始時のスケール。\n通常は 0〜1 の範囲で指定します。")]
    private float initialScale = 0.5f;

    [Space(15)]

    /// <summary>
    /// アニメーション終了時のスケール。通常は 1.0 が標準サイズです
    /// </summary>
    [SerializeField, Tooltip("アニメーション終了時のスケール。\n通常は 1.0 が標準サイズです。")]
    private float targetScale = 1.0f;

    [Space(15)]

    /// <summary>
    /// スケールアニメーションにかかる時間（秒単位）
    /// </summary>
    [SerializeField, Tooltip("スケールアニメーションにかかる時間（秒単位）。")]
    private float scaleDuration = 0.5f;

    #endregion


    #region 読み取り専用プロパティ(アニメーションのスケール設定の内部管理用変数)

    /// <summary>
    /// アニメーション開始時のスケールの読み取り専用
    /// </summary>
    internal float InitialScale => initialScale;

    /// <summary>
    /// アニメーション終了時のスケールの読み取り専用
    /// </summary>
    internal float TargetScale => targetScale;

    /// <summary>
    /// スケールアニメーションにかかる時間の読み取り専用
    /// </summary>
    internal float ScaleDuration => scaleDuration;

    #endregion

}
