using UnityEngine;
using TMPro;

[CreateAssetMenu(fileName = "KeyLabelConfig", menuName = "GameConfig/UI/キーラベル設定")]
public class KeyLabelConfig : ScriptableObject
{
    [SerializeField] private string[] keyLabels = { "S", "D", "F", "J", "K", "L" };
    [SerializeField] private float fontSize = 40f;
    [SerializeField] private TMP_FontAsset fontAsset;
    [SerializeField] private Color fontColor = Color.white;
    [SerializeField] private TextAlignmentOptions alignment = TextAlignmentOptions.Center;
    [SerializeField, Tooltip("レーンラベルのプレハブ (TextMeshPro付き)")]

    private GameObject laneLabelPrefab;

    [Header("▼ レーンラベル UI 調整")]
    [Tooltip("ラベルのY方向位置オフセット（下に動かす場合は正の値）")]
    [SerializeField] private float laneLabelYOffset = 60f;

    [Tooltip("ラベルのサイズ（Width, Height）")]
    [SerializeField] private Vector2 laneLabelSize = new Vector2(100f, 40f);


    [Header("▼ ラベル表示設定")]
    [SerializeField, Tooltip("レーンラベルの表示形式（例: Lane {0}）")]
    private string laneLabelFormat = "Lane {0}";
    public string[] KeyLabels => keyLabels;
    public float FontSize => fontSize;
    public TMP_FontAsset FontAsset => fontAsset;
    public Color FontColor => fontColor;
    public TextAlignmentOptions Alignment => alignment;

    internal float LaneLabelYOffset => laneLabelYOffset;
    internal Vector2 LaneLabelSize => laneLabelSize;


    internal string LaneLabelFormat => laneLabelFormat;

    
    internal GameObject LaneLabelPrefab => laneLabelPrefab;
}
