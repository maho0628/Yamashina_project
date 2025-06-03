using TMPro;
using UnityEngine;

public class JudgementTextUI : MonoBehaviour,IResultEntryUI
{
    /// <summary>
    /// 判定名を表示するためのTextMeshPro,
    /// </summary>
    [SerializeField,Header("判定名を表示するためのTextMeshPro")] private TextMeshProUGUI judgeNameText;

    /// <summary>
    /// その判定が出た数を表示するためのTextMeshPro
    /// </summary>
    [SerializeField, Header("その判定が出た数を表示するためのTextMeshPro")] private TextMeshProUGUI judgeCountText;

    private void Awake()
    {
        var texts = GetComponentsInChildren<TextMeshProUGUI>();
        foreach (var text in texts)
        {
            if (text.name.Contains("Name")) judgeNameText = text;
            else if (text.name.Contains("Count")) judgeNameText = text;
        }

        if (judgeNameText == null || judgeCountText == null)
        {
            Debug.LogWarning("[JudgementTextUI] Text コンポーネントの取得に失敗しました");
        }
    }
    public void Setup(string label, int value)
    {
        judgeNameText.text = label;
        judgeCountText.text = value.ToString();
    }

    public void SetValue(int value)
    {
        judgeCountText.text = value.ToString();
    }
}
