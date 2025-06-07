using UnityEngine;

[CreateAssetMenu(fileName = "NoteScrollConfig", menuName = "GameConfig/ノーツ/統合スクロール設定")]
public class NoteScrollConfig : ScriptableObject
{
   [SerializeField ,Header("ノーツのタイミングや位置")]
    private NoteTimingConfig timingConfig;

    [SerializeField, Header("レーンの見た目設定")]
    private LaneVisualConfig laneVisualConfig;

    [SerializeField, Header("キーラベル設定")]
    private KeyLabelConfig keyLabelConfig;


    internal NoteTimingConfig GetNoteTimingConfig() { return timingConfig; }    

    internal LaneVisualConfig GetLaneVisualConfig() { return laneVisualConfig; }    

    internal KeyLabelConfig GetKeyLabelConfig() { return keyLabelConfig; }  
}
