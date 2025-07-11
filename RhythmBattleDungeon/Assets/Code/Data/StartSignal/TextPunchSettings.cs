using DG.Tweening;
using UnityEngine;

/// <summary>
/// アニメーションのパンチアニメ設定
/// パンチのアニメーション使用時に設定
/// </summary>
[System.Serializable]
public class TextPunchSettings
{
    #region アニメーションのパンチアニメ設定の内部管理用変数

    /// <summary>
    /// パンチ時に加える力のベクトル（X, Y, Z）。動きの大きさを調整します。
    /// </summary>
    [SerializeField, Tooltip("パンチ時に加える力のベクトル（X, Y, Z）。\n動きの大きさを調整します。")]
    private Vector3 punchPower = new Vector3(0.2f, 0.2f, 0f);

    [Space(15)]

    /// <summary>
    /// パンチアニメーションの再生時間（秒単位）。
    /// </summary>
    [SerializeField, Tooltip("パンチアニメーションの再生時間（秒単位）。")]
    private float punchDuration = 0.6f;

    [Space(15)]

    /// <summary>
    /// パンチの振動の回数。大きいほど細かく震えます。
    /// </summary>
    [SerializeField, Tooltip("パンチの振動の回数。大きいほど細かく震えます。")]
    private int punchVibrato = 3;

    [Space(15)]

    /// <summary>
    /// アニメーションのイージングタイプ。動きの変化の仕方を決めます。
    /// </summary>
    [SerializeField,Tooltip("アニメーションのイージングタイプ。\n動きの変化の仕方を決めます。")]
    private Ease easeType = Ease.OutBack;

    #endregion


    #region 読み取り専用フィールド（アニメーションのパンチアニメ設定の内部管理用変数)

    /// <summary>
    ///  パンチ時に加える力のベクトルの読み取り専用
    /// </summary>
    internal Vector3 PunchPower => punchPower;

    /// <summary>
    /// パンチアニメーションの再生時間の読み取り専用
    /// </summary>
    internal float PunchDuration => punchDuration;

    /// <summary>
    ///  パンチの振動の回数の読み取り専用
    /// </summary>
    internal int PunchVibrato => punchVibrato;

    /// <summary>
    /// アニメーションのイージングタイプの読み取り専用
    /// </summary>
    internal Ease EaseType => easeType;

    #endregion

}
