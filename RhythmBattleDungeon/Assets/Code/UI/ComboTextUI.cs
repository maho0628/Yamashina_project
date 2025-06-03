using TMPro;
using UnityEngine;

public class ComboTextUI : MonoBehaviour, IResultEntryUI
{
    [SerializeField, Header("ƒRƒ“ƒ{‚ð•\Ž¦‚·‚é‚½‚ß‚ÌTextMeshPro")] private TextMeshProUGUI ComboNameText;
    private TextMeshProUGUI ComboCountText;

    private void Awake()
    {
        var texts = GetComponentsInChildren<TextMeshProUGUI>();
        foreach (var text in texts)
        {
            if (text.name.Contains("Name")) ComboNameText = text;
            else if (text.name.Contains("Count")) ComboCountText = text;
        }
    }

    public void Setup(string label, int value)
    {
        ComboNameText.text = label;
        ComboCountText.text = value.ToString();
    }

    public void SetValue(int value)
    {
        ComboCountText.text = value.ToString();
    }
}
