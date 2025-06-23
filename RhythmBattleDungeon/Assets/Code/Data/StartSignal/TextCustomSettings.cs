using UnityEngine;

/// <summary>
/// カスタムアニメーション設定クラス
/// カスタムタイプ使用時に使用
/// </summary>
[System.Serializable]
public class TextCustomSettings
{
    /// <summary>
    /// カスタム時のアニメーションクリップ
    /// </summary>
    [SerializeField,Tooltip("カスタム時のアニメーションクリップ")]
    private AnimationClip customAnimationClip;

    /// <summary>
    /// カスタム時のアニメーションクリップの読み取り専用
    /// </summary>
    internal AnimationClip CustomAnimationClip => customAnimationClip;
}


