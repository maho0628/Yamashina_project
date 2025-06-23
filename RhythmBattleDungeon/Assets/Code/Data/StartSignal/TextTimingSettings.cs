using UnityEngine;

[System.Serializable]
public class TextTimingSettings
{
    [SerializeField] private float fadeInDuration = 0.3f;
    [SerializeField] private float fadeInAlpha = 1f;
    [SerializeField] private float displayDuration = 1.0f;
    [SerializeField] private float fadeOutDuration = 0.3f;
    [SerializeField] private float fadeOutAlpha = 0f;

    public float FadeInDuration => fadeInDuration;
    public float FadeInAlpha => fadeInAlpha;
    public float DisplayDuration => displayDuration;
    public float FadeOutDuration => fadeOutDuration;
    public float FadeOutAlpha => fadeOutAlpha;
}
