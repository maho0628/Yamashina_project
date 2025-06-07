using UnityEngine;

/// <summary>
/// ゲージのアニメーション設定
/// </summary>
[System.Serializable]
public class GaugeAnimationConfig
{
    [SerializeField, Tooltip("ゲージ補間時間（秒）")]
    private float gaugeLerpDuration = 0.5f;

    [SerializeField, Tooltip("補間にEasingを使うか")]
    private bool useEasing = false;

    [SerializeField, Tooltip("ゲージアニメーションに使う補間カーブ（イージング）")]
    private AnimationCurve gaugeAnimationCurve = AnimationCurve.Linear(0, 0, 1, 1);

    [SerializeField, Tooltip("スコア上昇時の演出カラー")]
    private Color scoreGainColor = Color.green;

    [SerializeField, Tooltip("MAX時にゲージをフラッシュさせるか")]
    private bool flashOnFull = false;

    [SerializeField, Tooltip("MAX時の演出エフェクト")]
    private GameObject flashEffectPrefab;

    public float GaugeLerpDuration => gaugeLerpDuration;
    public bool UseEasing => useEasing;
    public AnimationCurve GaugeAnimationCurve => gaugeAnimationCurve;
    public Color ScoreGainColor => scoreGainColor;
    public bool FlashOnFull => flashOnFull;
    public GameObject FlashEffectPrefab => flashEffectPrefab;
}
