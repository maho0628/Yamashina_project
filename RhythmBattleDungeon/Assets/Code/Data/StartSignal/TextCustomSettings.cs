using UnityEngine;

/// <summary>
/// カスタムアニメーション設定クラス
/// カスタムタイプ使用時に使用
/// </summary>
[System.Serializable]
public class TextCustomSettings
{
    /// <summary>
    /// AnimatorControllerの基本となるアセット。
    /// Animatorに適用されるコントローラー（ステートや遷移などを定義）。
    /// </summary>
    [SerializeField, Tooltip("カスタム時のアニメーションコントローラー")]
    private RuntimeAnimatorController baseAnimatorController;

    [Space(15)]

    /// <summary>
    /// AnimatorController内の元クリップと差し替え先のペア
    /// </summary>
    [SerializeField, Tooltip("AnimatorController内の元クリップと差し替え先のペア")]
    private AnimationOverridePair[] overridePairs;

    /// <summary>
    /// アニメーションコントローラーの取得の読み取り専用。
    /// </summary>
    internal RuntimeAnimatorController BaseAnimatorController
    {
        get { return baseAnimatorController; }
    }

    internal AnimationOverridePair[] OverridePairs
    {
        get { return overridePairs; }
    }


    /// <summary>
    /// AnimatorController 内のアニメーションクリップを別のクリップに差し替えるための設定を保持するクラス。
    /// 1つの差し替え対象（元のクリップ名）と、差し替える先（新しい AnimationClip）をペアで保持します。
    /// </summary>
    [System.Serializable]
    public class AnimationOverridePair
    {
        /// <summary>
        /// カスタム時のアニメーションクリップ
        /// </summary>
        [SerializeField, Tooltip("カスタム時の差し替え対象となるAnimatorController内のクリップ")]
        private AnimationClip originalClip;

        [Space(15)]

        /// <summary>
        /// 差し替える先のクリップ
        /// </summary>
        [SerializeField, Tooltip("差し替える先のクリップ")]
        private AnimationClip overrideClip;

        [Space(15)]

        /// <summary>
        /// 再生対象ステート名
        /// </summary>
        [SerializeField, Tooltip("再生対象ステート名")]

        private string targetStateName;

        /// <summary>
        /// カスタム時の差し替え対象となるAnimatorController内のクリップ
        /// </summary>
        internal AnimationClip OriginalClip => originalClip;

        /// <summary>
        /// 差し替える先のクリップ
        /// </summary>
        internal AnimationClip OverrideClip => overrideClip;
        
        internal string TargetStateName { get { return targetStateName; } } 

    }

}


