using UnityEngine;
[System.Serializable]
public class JudgementEffectConfig 
{

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
