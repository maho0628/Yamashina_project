using UnityEngine;

/// <summary>
/// レーンの見た目を決めるスクリプタブルオブジェクト
/// </summary>
[CreateAssetMenu(fileName = "LaneVisualConfig", menuName = "GameConfig/ノーツ/レーン見た目設定")]
public class LaneVisualConfig : ScriptableObject
{
    #region レーンの見た目を決める内部管理用変数

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



    [Header("▼ レーン生成用プレハブ設定")]

    [SerializeField, Tooltip("レーンの背景画像プレハブ (Image付き)")]
    private GameObject laneImagePrefab;


    #endregion

    #region  読み取り専用プロパティ (レーンの見た目を決める内部管理用変数)


    internal float LaneWidth => laneWidth;
    internal float LaneHeight => laneHeight;
    internal int LaneCount => laneCount;

    
    internal Color GetLaneColor(int index) =>
        (index >= 0 && index < laneColors.Length) ? laneColors[index] : Color.white;

    internal Sprite GetLaneSprite(int index) =>
        (index >= 0 && index < laneSprites.Length) ? laneSprites[index] : null;
    internal GameObject LaneImagePrefab => laneImagePrefab;
    #endregion

}
