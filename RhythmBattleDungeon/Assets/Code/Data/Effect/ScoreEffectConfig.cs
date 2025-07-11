using UnityEngine;

//// <summary>
/// スコア演出に使用する視覚効果設定クラス。<br/>
/// スコア数表示のテキスト、アニメーションスケール、透明度の初期値・終了値などを設定可能。
/// 参照先変更大エフェクト形をまとめたスクリプタブルオブジェクト制作予定
/// </summary>
[System.Serializable]
public class ScoreEffectConfig
{
    #region  スコアテキストフォーマットの内部情報処理変数

    /// <summary>
    ///スコア数を表示する際のテキストフォーマット。
    /// 例: "Score: {0}!" → Score: 25! のように表示される。
    /// </summary>
    [Header("▼スコア演出テキストフォーマット")]
    [SerializeField, Tooltip(" スコア数を表示するテキストフォーマット。{0}が    スコア数に置き換わります")]
    private string scoreTextFormat = "Score: {0}!";

    #endregion

    [Space(15)]

    #region スコア演出のスケール関連の内部情報処理変数

    /// <summary>
    /// アニメーション開始時のスケール（通常は Vector3.zero で拡大演出をする）。
    /// </summary>
    [Header("▼スケール設定")]
    [SerializeField, Tooltip("演出開始時のスケール")]
    private Vector3 startScale = Vector3.zero;

    [Space(15)]

    /// <summary>
    /// アニメーション終了時のスケール（通常は Vector3.one で元のサイズに戻す）。
    /// </summary>
    [SerializeField, Tooltip("演出終了時のスケール")]
    private Vector3 endScale = Vector3.one;

    #endregion

    [Space(15)]

    #region スコア演出の透明度関連の内部情報処理変数

    /// <summary>
    /// 表示開始時の透明度（1 = 完全に表示、0 = 完全に透明）。
    /// </summary>
    [Header("▼アルファ設定")]
    [SerializeField, Tooltip("表示開始時の透明度（1 = 完全に表示）")]
    private float startAlpha = 1f;

    [Space(15)]

    /// <summary>
    /// フェードアウト終了時の透明度（0 = 完全に非表示）。
    /// </summary>
    [SerializeField, Tooltip("フェードアウト終了時の透明度（0 = 完全に非表示）")]
    private float endAlpha = 0f;

    #endregion


    #region 読み取り専用フィールド( スコアテキストフォーマットの内部情報処理変数)

    /// <summary>
    /// スコア演出テキストフォーマットの読み取り専用
    /// </summary>
    internal string ScoreTextFormat => scoreTextFormat;

    #endregion


    #region  読み取り専用フィールド(スコア演出のスケール関連の内部情報処理変数)

    /// <summary>
    /// アニメーション開始時のスケールの読み取り専用
    /// </summary>
    internal Vector3 StartScale => startScale;

    /// <summary>
    /// アニメーション終了時のスケールの読み取り専用
    /// </summary>
    internal Vector3 EndScale => endScale;

    #endregion


    #region  読み取り専用フィールド(スコア演出の透明度関連の内部情報処理変数)

    /// <summary>
    /// 表示開始時の透明度の読み取り専用
    /// </summary>
    internal float StartAlpha => startAlpha;

    /// <summary>
    /// フェードアウト終了時の透明度の読み取り専用
    /// </summary>
    internal float EndAlpha => endAlpha;

    #endregion

}
