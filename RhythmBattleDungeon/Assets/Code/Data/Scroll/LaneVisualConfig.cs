using UnityEngine;

/// <summary>
/// レーンの見た目を決めるスクリプタブルオブジェクト
/// レーンサイズやレーンの色などを設定します
/// </summary>
[CreateAssetMenu(fileName = "LaneVisualConfig", menuName = "GameConfig/ノーツ/レーン見た目設定")]
public class LaneVisualConfig : ScriptableObject
{
    #region レーンの見た目を決める内部管理用変数

    /// <summary>
    /// 各レーンの横幅（px）
    /// </summary>
    [Header("▼レーンサイズ設定")]

    [SerializeField, Tooltip("各レーンの横幅（px）")]
    private float laneWidth = 100f;

    [Space(15)]

    /// <summary>
    /// 各レーンの高さ
    /// </summary>
    [SerializeField, Tooltip("各レーンの高さ")]
    private float laneHeight;

    [Space(15)]

    /// <summary>
    /// レーン数。初期設定は4
    /// </summary>
    [Header("▼レーン構成設定")]

    [SerializeField, Tooltip("レーン数。初期設定は4")]
    [Min(1)]
    private int laneCount = 4;

    [Space(15)]

    /// <summary>
    ///レーンの背景画像プレハブ (Image付き)
    /// </summary>
    [Header("▼ レーン生成用プレハブ設定")]

    [SerializeField, Tooltip("レーンの背景画像プレハブ (Image付き)")]
    private GameObject laneImagePrefab;

    [Space(15)]

    /// <summary>
    /// 各レーンに割り当てる色（インデックスで指定）
    /// </summary>
    [Header("▼レーンビジュアル設定")]

    [SerializeField, Tooltip("各レーンに割り当てる色（インデックスで指定）")]
    private Color[] laneColors;

    [Space(15)]

    /// <summary>
    /// 各レーンに割り当てるスプライト画像（インデックスで指定）
    /// </summary>
    [SerializeField, Tooltip("各レーンに割り当てるスプライト画像（インデックスで指定）")]
    private Sprite[] laneSprites;


    #endregion

    #region  読み取り専用プロパティ (レーンの見た目を決める内部管理用変数)

    /// <summary>
    /// 各レーンの横幅（px）の読み取り専用
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

    /// <summary>
    /// レーンの背景画像プレハブ (Image付き)  の読み取り専用
    /// </summary>
    internal GameObject LaneImagePrefab => laneImagePrefab;

    #endregion


    #region ゲッター

    /// <summary>
    /// 対応するレーンの色を返す
    /// </summary>
    /// <param name="index">レーン番号</param>
    /// <returns>レーンの色</returns>
    internal Color GetLaneColor(int index) =>
        (index >= 0 && index < laneColors.Length) ? laneColors[index] : Color.white;

    /// <summary>
    /// 対応するレーンの画像を返す
    /// </summary>
    /// <param name="index">レーン番号</param>
    /// <returns>レーンのSprite</returns>
    internal Sprite GetLaneSprite(int index) =>
        (index >= 0 && index < laneSprites.Length) ? laneSprites[index] : null;

    /// <summary>
    /// laneContainerの幅と設定されたレーン数・幅から、左端のレーンのX座標（中央配置）を返す
    /// </summary>
    /// <param name="containerWidth">laneContainer の RectTransform の幅</param>
    /// <returns>1レーン目の中央の X 座標</returns>
    internal float GetStartX(float containerWidth) =>
        -containerWidth / 2f + laneWidth / 2f;


    #endregion

}
