using UnityEngine;

[System.Serializable]
public class TextScaleSettings
{
    [SerializeField] private float initialScale = 0.5f;
    [SerializeField] private float targetScale = 1.0f;
    [SerializeField] private float scaleDuration = 0.5f;

    public float InitialScale => initialScale;
    public float TargetScale => targetScale;
    public float ScaleDuration => scaleDuration;
}
