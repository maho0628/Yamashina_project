using UnityEngine;

/// <summary>
/// ノーツのスクロール設定に関するスクリプタブルオブジェクト
/// </summary>
[CreateAssetMenu(
    fileName = "NoteScrollConfig",
    menuName = "GameConfig/ノーツ/スクロール設定"

)]
public class NoteScrollConfig : ScriptableObject
{
    #region ノーツスクロール設定に関する内部管理用変数


    /// <summary>
    /// ノーツが判定ラインに到達するまでの時間（秒）
    /// </summary>
    [Tooltip("ノーツスクロール設定")]
    [SerializeField, Header("ノーツが判定ラインに到達するまでの時間（秒）,1～２秒の間が妥当")]
    private float scrollDuration = 3f;

    /// <summary>
    /// ノーツの出現Y座標（上）
    /// </summary>
    [SerializeField, Tooltip("ノーツの出現Y座標（上）")]
    private float startY = 500f;


    /// <summary>
    /// ノーツの終了Y座標（下）
    /// </summary>
    [SerializeField, Header("ノーツの終了Y座標（下）")]
    private float endY = -100f;

    #endregion


    #region レーンの情報に関する内部管理用変数

    /// <summary>
    /// 各レーンの横幅（px）
    /// </summary>
    [SerializeField, Header("各レーンの横幅（px）")]
    private float laneWidth = 100f;

      /// <summary>
    /// 各レーンの高さ
    /// </summary>
    [SerializeField, Header("各レーンの高さ")]
    private float laneHeight;

    /// <summary>
    /// レーンの色を設定
    /// </summary>
    [SerializeField, Header("レーンの色を設定")]
    private Color[] laneColors;

    /// <summary>
    /// レーンごとの画像
    /// </summary>
    [SerializeField, Header("レーンごとの画像")]
    private Sprite[] laneSprites;

    /// <summary>
    /// レーン数。初期設定は4
    /// </summary>
    [SerializeField, Header("レーン数。初期設定は4")]
    [Min(1)]
    private int laneCount = 4;

    #endregion


    #region 読み取り専用プロパティ(ノーツスクロール設定に関する内部管理用変数)

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


    #region 読み取り専用プロパティ(レーンの情報に関する内部管理用変数)

    /// <summary>
    ///  各レーンの横幅（px）の読み取り専用
    /// </summary>
    internal float LaneWidth => laneWidth;

    /// <summary>
    /// 各レーンの高さの読み取り専用
    /// </summary>
    internal float LaneHeight => laneHeight;

    /// <summary>
    /// レーン数の読み取り専用
    /// </summary>
    internal int LaneCount => laneCount;

    #endregion


    #region ゲッターメソッド

    /// <summary>
    /// laneIndexのレーンの色を返す
    /// </summary>
    /// <param name="laneIndex"></param>
    /// <returns>Color</returns>
    internal Color GetLaneColor(int laneIndex)
    {
        if (laneIndex >= 0 && laneIndex < laneColors.Length)
            return laneColors[laneIndex];
        return Color.white;
    }

    /// <summary>
    /// laneIndexのレーンごとの画像を返す
    /// </summary>
    /// <param name="laneIndex"></param>
    /// <returns>Sprite</returns>
    internal Sprite GetLaneSprite(int laneIndex)
    {
        if (laneIndex >= 0 && laneIndex < laneSprites.Length)
            return laneSprites[laneIndex];
        return null;
    }

    #endregion
}


