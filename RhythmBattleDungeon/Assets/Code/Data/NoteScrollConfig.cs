using UnityEngine;

/// <summary>
/// ノーツのスクロール設定に関するスクリプタブルオブジェクト
/// </summary>
[CreateAssetMenu(
    fileName = "NoteScrollConfig",
    menuName = "GameConfig/ノーツ/スクロール設定"
)]
[System.Serializable]
public class NoteScrollConfig : ScriptableObject
{
    [Header("ノーツスクロール設定")]

    [SerializeField, Tooltip("ノーツが判定ラインに到達するまでの時間（秒）")]
    private float scrollDuration = 3f;

    [SerializeField, Tooltip("ノーツの出現Y座標（上）")]
    private float startY = 500f;

    [SerializeField, Tooltip("ノーツの終了Y座標（下）")]
    private float endY = -100f;

    [SerializeField, Tooltip("各レーンの横幅（px）")]
    private float laneWidth = 100f;

    [SerializeField, Tooltip("各レーンの高さ")]
    private float laneHeight; // レーンの幅と高さを設定

    [SerializeField, Tooltip("レーンの色を設定")]
    private Color[] laneColors; // ここでレーンの色を設定

    [SerializeField, Tooltip("レーンごとの画像")]
    private Sprite[] laneSprites; // ここでレーンごとの画像を設定

    [Tooltip("レーン数。通常は4。プランナーが変更可能です")]
    [SerializeField, Min(1)]
    private int laneCount = 4;
 
    // 外部から読み取り専用でアクセスできるようにする
    internal float ScrollDuration => scrollDuration;
    internal float StartY => startY;
    internal float EndY => endY;
    internal float LaneWidth => laneWidth;
    internal float LaneHeight => laneHeight;

    internal int LaneCount => laneCount;

    internal Color GetLaneColor(int laneIndex)
    {
        if (laneIndex >= 0 && laneIndex < laneColors.Length)
        {
            return laneColors[laneIndex];
        }
        return Color.white; // デフォルトの色
    }
    // レーンの画像を取得
    internal Sprite GetLaneSprite(int laneIndex)
    {
        if (laneIndex >= 0 && laneIndex < laneSprites.Length)
        {
            return laneSprites[laneIndex];
        }
        return null; // デフォルトは null（設定されていない場合）
    }
}


