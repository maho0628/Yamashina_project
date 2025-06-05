using TMPro;
using UnityEngine;

public class ComboTextUI : MonoBehaviour, IResultEntryUI
{
    [SerializeField, Header("Max Comboテキスト表示用TextMeshPro")]
    private TextMeshProUGUI comboNameText;
    [SerializeField, Header("コンボ数を表示するためのTextMeshPro")] private TextMeshProUGUI comboCountText;

    private void Awake()
    {
        var texts = GetComponentsInChildren<TextMeshProUGUI>();
        foreach (var text in texts)
        {
            if (text.name.Contains("Name")) comboNameText = text;
            else if (text.name.Contains("Count")) comboCountText = text;
        }
    }

    public void Setup(string label, int value)
    {
        comboNameText.text = label;
        comboCountText.text = value.ToString();
    }

    public void SetValue(int value)
    {
        comboCountText.text = value.ToString();
    }
}
