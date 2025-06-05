using UnityEngine;

[CreateAssetMenu(fileName = "NoteTimingConfig", menuName = "GameConfig/ノーツ/スクロールタイミング")]
public class NoteTimingConfig : ScriptableObject
{
    [SerializeField, Header("ノーツが判定ラインに到達するまでの時間（秒）")]
    private float scrollDuration = 3f;

    [SerializeField, Header("ノーツの出現Y座標（上）")]
    private float startY = 500f;

    [SerializeField, Header("ノーツの終了Y座標（下）")]
    private float endY = -100f;

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
}
