using UnityEngine;

/// <summary>
/// デバッグ用の設定
/// </summary>
[System.Serializable]
public class GaugeDebugConfig
{
    [SerializeField, Tooltip("スコア変化しなくても常にゲージをアニメさせる")]
    private bool debugAlwaysAnimate = false;

    [SerializeField, Tooltip("初期ゲージ値（0〜1）")]
    [Range(0f, 1f)]
    private float debugInitialValue = 0f;

    public bool DebugAlwaysAnimate => debugAlwaysAnimate;
    public float DebugInitialValue => debugInitialValue;
}
