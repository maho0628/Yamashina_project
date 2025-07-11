using UnityEngine;

/// <summary>
/// スコアゲージの設定全体をまとめるScriptableObject
/// </summary>
[CreateAssetMenu(fileName = "GaugeConfig", menuName = "GameConfig/GaugeConfig")]
public class GaugeConfig : ScriptableObject
{
    #region ゲージの設定全体に関連する内部管理用変数　

    /// <summary>
    /// ゲージのアニメーション設定
    /// </summary>
    [Header("▼【アニメーション】ゲージの動き方を設定")]

    [SerializeField, Tooltip("ゲージが変化する際の動き（補間時間やEasingカーブ）を設定します。")]
    private GaugeAnimationConfig animationConfig;

    [Space(15)]

    /// <summary>
    /// ゲージの見た目設定
    /// </summary>
    [Header("▼【ビジュアル】ゲージの見た目を設定")]

    [SerializeField, Tooltip("ゲージの色やしきい値ごとの色分けを設定します。")]
    private GaugeVisualConfig visualConfig;

    [Space(15)]

    /// <summary>
    /// ゲージのデバッグ設定
    /// </summary>
    [Header("▼【デバッグ】初期ゲージ値や挙動の確認用設定")]

    [SerializeField, Tooltip("ゲーム実行中のデバッグ用初期値などを設定します。")]
    private GaugeDebugConfig debugConfig;


    #endregion


    #region 読み取り専用プロパティ (ゲージの設定全体に関連する内部管理用変数)

    /// <summary>
    /// ゲージのアニメーション設定の読み取り専用プロパティ
    /// </summary>
    internal GaugeAnimationConfig Animation => animationConfig;

    /// <summary>
    /// ゲージの見た目設定の読み取り専用プロパティ
    /// </summary>
    internal GaugeVisualConfig Visual => visualConfig;

    /// <summary>
    /// ゲージのデバッグ設定の読み取り専用プロパティ
    /// </summary>
    internal GaugeDebugConfig Debug => debugConfig;

    #endregion
}
