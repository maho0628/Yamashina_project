using UnityEngine;
using TMPro;

[CreateAssetMenu(fileName = "KeyLabelConfig", menuName = "GameConfig/UI/ƒL[ƒ‰ƒxƒ‹Ý’è")]
public class KeyLabelConfig : ScriptableObject
{
    [SerializeField] private string[] keyLabels = { "S", "D", "F", "J", "K", "L" };
    [SerializeField] private float fontSize = 40f;
    [SerializeField] private TMP_FontAsset fontAsset;
    [SerializeField] private Color fontColor = Color.white;
    [SerializeField] private TextAlignmentOptions alignment = TextAlignmentOptions.Center;

    public string[] KeyLabels => keyLabels;
    public float FontSize => fontSize;
    public TMP_FontAsset FontAsset => fontAsset;
    public Color FontColor => fontColor;
    public TextAlignmentOptions Alignment => alignment;
}
