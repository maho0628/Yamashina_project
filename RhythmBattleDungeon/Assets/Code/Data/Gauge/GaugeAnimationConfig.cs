using UnityEngine;

/// <summary>
/// ゲージのアニメーション設定
/// </summary>
[System.Serializable]
public class GaugeAnimationConfig
{
    #region ゲージのアニメーションに関連する内部管理用変数　

    /// <summary>
    /// ゲージ補間時間
    [Header("▼ゲージ補間設定"), Range(0f, 1f)]
    [SerializeField, Tooltip("ゲージ補間時間（秒）")]
    private float gaugeLerpDuration = 0.5f;

    [Space(15)]

    /// <summary>
    /// 補間にEasingを使うか
    /// </summary>
    [SerializeField, Tooltip("補間にEasingを使うか")]
    private bool useEasing = false;

    [Space(15)]

    /// <summary>
    /// ゲージアニメーションに使う補間カーブ（イージング）
    /// </summary>
    [SerializeField, Tooltip("ゲージアニメーションに使う補間カーブ（イージング）")]
    private AnimationCurve gaugeAnimationCurve = AnimationCurve.Linear(0, 0, 1, 1);


    #endregion


    #region 読み取り専用プロパティ (ゲージのアニメーションに関連する内部管理用変数)

    /// <summary>
    /// ゲージ補間時間（秒）の読み取り専用
    /// </summary>
    internal float GaugeLerpDuration => gaugeLerpDuration;

    /// <summary>
    /// 補間にEasingを使うかの読み取り専用
    /// </summary>
    internal bool UseEasing => useEasing;

    /// <summary>
    ///  ゲージアニメーションに使う補間カーブ（イージング）の読み取り専用
    /// </summary>
    internal AnimationCurve GaugeAnimationCurve => gaugeAnimationCurve;


    #endregion

}
