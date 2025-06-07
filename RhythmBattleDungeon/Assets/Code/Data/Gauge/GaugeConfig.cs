using UnityEngine;

/// <summary>
/// スコアゲージの設定全体をまとめるScriptableObject
/// </summary>
[CreateAssetMenu(fileName = "GaugeConfig", menuName = "GameConfig/GaugeConfig")]
public class GaugeConfig : ScriptableObject
{
    [Header("■ アニメーション設定")]
    [SerializeField]
    private GaugeAnimationConfig animationConfig;

    [Header("■ UIの見た目設定")]
    [SerializeField]
    private GaugeVisualConfig visualConfig;

    [Header("■ デバッグ設定")]
    [SerializeField]
    private GaugeDebugConfig debugConfig;

    public GaugeAnimationConfig Animation => animationConfig;
    public GaugeVisualConfig Visual => visualConfig;
    public GaugeDebugConfig Debug => debugConfig;
}
