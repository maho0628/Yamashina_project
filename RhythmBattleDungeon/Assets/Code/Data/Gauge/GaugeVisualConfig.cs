using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// ゲージの見た目設定
/// </summary>
[System.Serializable]
public class GaugeVisualConfig
{
    [SerializeField, Tooltip("ゲージの背景色")]
    private Color gaugeBackgroundColor = Color.gray;

    [SerializeField, Tooltip("ゲージの基本塗り色")]
    private Color gaugeFillColor = Color.cyan;

    [SerializeField, Tooltip("ゲージの割合に応じて色を変える")]
    private List<ThresholdColor> thresholdColors;

    public Color GaugeBackgroundColor => gaugeBackgroundColor;
    public Color GaugeFillColor => gaugeFillColor;
    public List<ThresholdColor> ThresholdColors => thresholdColors;

    [System.Serializable]
    public class ThresholdColor
    {
        [Range(0f, 1f)]
        public float threshold;

        public Color gaugeColor;
    }
}
