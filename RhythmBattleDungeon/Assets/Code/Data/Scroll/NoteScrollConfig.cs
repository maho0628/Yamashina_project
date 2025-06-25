using UnityEngine;

/// <summary>
/// 統合スクロール設定のスクリプタブルオブジェクト
/// ノーツのタイミングや位置の設定やレーンの見た目設定、キーラベル設定を管理
/// </summary>
[CreateAssetMenu(fileName = "NoteScrollConfig", menuName = "GameConfig/ノーツ/統合スクロール設定")]
public class NoteScrollConfig : ScriptableObject
{
    #region  統合スクロール設定の内部管理用変数

    /// <summary>
    /// ノーツのタイミングや位置の設定
    /// </summary>
    [SerializeField, Header("▼ノーツのタイミングや位置の設定")]
    private NoteTimingConfig timingConfig;

    [Space(15)]

    /// <summary>
    /// レーンの見た目設定
    /// </summary>
    [SerializeField, Header("▼レーンの見た目設定")]
    private LaneVisualConfig laneVisualConfig;

    [Space(15)]

    /// <summary>
    /// キーラベル設定
    /// </summary>
    [SerializeField, Header("▼キーラベル設定")]
    private KeyLabelConfig keyLabelConfig;

    #endregion


    #region ゲッター

    /// <summary>
    /// ノーツのタイミングや位置の設定を返す
    /// </summary>
    /// <returns>NoteTimingConfig</returns>
    internal NoteTimingConfig GetNoteTimingConfig() { return timingConfig; }

    /// <summary>
    /// レーンの見た目設定を返す
    /// </summary>
    /// <returns>LaneVisualConfig</returns>
    internal LaneVisualConfig GetLaneVisualConfig() { return laneVisualConfig; }

    /// <summary>
    /// キーラベル設定 を返す
    /// </summary>
    /// <returns>KeyLabelConfig</returns>
    internal KeyLabelConfig GetKeyLabelConfig() { return keyLabelConfig; }

    #endregion

}
