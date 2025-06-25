using UnityEngine;

//// <summary>
/// コンボ演出に使用する視覚効果設定クラス。<br/>
/// コンボ数表示のテキスト、アニメーションスケール、透明度の初期値・終了値などを設定可能。
/// 参照先変更大エフェクト形をまとめたスクリプタブルオブジェクト制作予定
/// </summary>
[System.Serializable]
public class ComboEffectConfig
{
    #region コンボテキストフォーマットの内部情報処理変数

    /// <summary>
    /// コンボ数を表示する際のテキストフォーマット。
    /// 例: "Combo: {0}!" → Combo: 25! のように表示される。
    /// </summary>
    [Header("▼コンボ演出テキストフォーマット")]
    [SerializeField, Tooltip("コンボ数を表示するテキストフォーマット。{0}がコンボ数に置き換わります")]
    private string comboTextFormat = "Combo: {0}!";

    #endregion

    [Space(15)]

    #region コンボ演出のスケール関連の内部情報処理変数

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

    #region  コンボ演出の透明度関連の内部情報処理変数

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


    #region 読み取り専用フィールド(コンボテキストフォーマットの内部情報処理変数)

    /// <summary>
    /// コンボ演出テキストフォーマットの読み取り専用
    /// </summary>
    internal string ComboTextFormat => comboTextFormat;

    #endregion


    #region  読み取り専用フィールド(コンボ演出のスケール関連の内部情報処理変数)

    /// <summary>
    /// アニメーション開始時のスケールの読み取り専用
    /// </summary>
    internal Vector3 StartScale => startScale;

    /// <summary>
    /// アニメーション終了時のスケールの読み取り専用
    /// </summary>
    internal Vector3 EndScale => endScale;

    #endregion


    #region  読み取り専用フィールド(コンボ演出の透明度関連の内部情報処理変数)

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
