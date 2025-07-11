using UnityEngine;

/// <summary>
/// デバッグ用の設定
/// </summary>
[System.Serializable]
public class GaugeDebugConfig
{
    #region デバッグ用の設定に関連する内部管理用変数

    /// <summary>
    /// スコア変化しなくても常にゲージをアニメさせるかどうか
    /// </summary>
    [Header("▼デバッグ設定")]

    [SerializeField, Tooltip("スコア変化しなくても常にゲージをアニメさせるかどうか")]
    private bool debugAlwaysAnimate = false;

    [Space(15)]

    [Range(0f, 1f)]
    [SerializeField, Tooltip("初期ゲージ値（0〜1）")]
    private float debugInitialValue = 0f;

    #endregion


    #region 読み取り専用プロパティ(デバッグ用の設定に関連する内部管理用変数)

    /// <summary>
    /// スコア変化しなくても常にゲージをアニメさせるかどうかの読み取り専用プロパティ
    /// </summary>
    internal bool DebugAlwaysAnimate => debugAlwaysAnimate;

    /// <summary>
    ///  初期デバッグ用のゲージ値（0〜1）の読み取り専用プロパティ
    /// </summary>
    internal float DebugInitialValue => debugInitialValue;

    #endregion

}
