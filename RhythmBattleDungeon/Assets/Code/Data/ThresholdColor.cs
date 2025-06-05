using UnityEngine;

[System.Serializable]
public class ThresholdColor
{
    [Range(0f, 1f)]
    public float threshold;

    public Color color;
}
