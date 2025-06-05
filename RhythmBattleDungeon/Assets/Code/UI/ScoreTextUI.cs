using TMPro;
using UnityEngine;


public class ScoreTextUI : MonoBehaviour, IResultEntryUI
{
 
    [SerializeField, Header("ハイスコアの表示名を表示するためのTextMeshPro")] private TextMeshProUGUI scoreNameText;
    [SerializeField, Header("ハイスコアを表示するためのTextMeshPro")] private TextMeshProUGUI scoreCountText;

    private void Awake()
    {
        var texts = GetComponentsInChildren<TextMeshProUGUI>();
        foreach (var text in texts)
        {
            if (text.name.Contains("Name")) scoreNameText = text;
            else if (text.name.Contains("Count")) scoreCountText = text;
        }
    }

    public void Setup(string label, int value)
    {
        scoreNameText.text = label;
        scoreCountText.text = value.ToString();
    }

    public void SetValue(int value)
    {
        scoreCountText.text = value.ToString();
    }
}

// Update is called once per frame
