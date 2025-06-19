using UnityEngine;

/// <summary>
/// スクロールタイミングに関するスクリプタブルオブジェクト
/// ノーツの出現Y座標やノーツが判定ラインに到達するまでの時間などを管理する
/// </summary>
[CreateAssetMenu(fileName = "NoteTimingConfig", menuName = "GameConfig/ノーツ/スクロールタイミング")]
public class NoteTimingConfig : ScriptableObject
{
    #region スクロールタイミングに関する内部管理用変数

    /// <summary>
    /// ノーツが判定ラインに到達するまでの時間(秒）
    /// </summary>
    [SerializeField, Tooltip("ノーツが判定ラインに到達するまでの時間（秒）")]
    private float scrollDuration = 3f;

    [Space(15)]

    /// <summary>
    /// ノーツの出現Y座標（上）"
    /// </summary>
    [SerializeField, Tooltip("ノーツの出現Y座標（上）")]
    private float startY = 500f;

    [Space(15)]

    /// <summary>
    /// ノーツの終了Y座標（下）
    /// </summary>
    [SerializeField, Tooltip("ノーツの終了Y座標（下）")]
    private float endY = -100f;

    #endregion


    #region 読み取り専用プロパティ(スクロールタイミングに関する内部管理用変数)

    /// <summary>
    /// ノーツが判定ラインに到達するまでの時間（秒）の読み取り専用
    /// </summary>
    internal float ScrollDuration => scrollDuration;

    /// <summary>
    /// ノーツの出現Y座標（上）の読み取り専用
    /// </summary>
    internal float StartY => startY;

    /// <summary>
    /// ノーツの終了Y座標（下）の読み取り専用
    /// </summary>
    internal float EndY => endY;

    #endregion

}
