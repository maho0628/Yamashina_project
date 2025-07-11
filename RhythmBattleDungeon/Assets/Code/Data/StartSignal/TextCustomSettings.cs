using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// カスタムアニメーション設定クラス
/// カスタムタイプ使用時に使用
/// </summary>
[System.Serializable]
public class TextCustomSettings
{
    #region カスタムアニメーション設定の内部管理用変数

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
    private List<AnimationOverridePair> overridePairs;

    #endregion


    #region 読み取り専用フィールド(カスタムアニメーション設定の内部管理用変数)

    /// <summary>
    /// アニメーションコントローラーの取得の読み取り・取得
    /// </summary>
    public RuntimeAnimatorController BaseAnimatorController
    {
        get { return baseAnimatorController; }
        set { baseAnimatorController = value; }
    }

    /// <summary>
    //アニメーションクリップを別のクリップに差し替えるための設定を保持するクラスの読み取り
    /// </summary>
    public List<AnimationOverridePair> OverridePairs
    {
        get { return overridePairs; }
    }

    #endregion


    /// <summary>
    /// AnimatorController 内のアニメーションクリップを別のクリップに差し替えるための設定を保持するクラス。
    /// 1つの差し替え対象（元のクリップ名）と、差し替える先（新しい AnimationClip）をペアで保持します。
    /// </summary>
    [System.Serializable]
    public class AnimationOverridePair
    {
        #region アニメーションクリップを別のクリップに差し替えるための設定の内部管理用変数

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
        [SerializeField, AnimatorStateDropdown("BaseAnimatorController")]
        private string targetStateName;

        #endregion


        #region アニメーションクリップを別のクリップに差し替えるための設定の内部管理用変数

        /// <summary>
        /// カスタム時の差し替え対象となるAnimatorController内のクリップの読み取り専用
        /// </summary>
        internal AnimationClip OriginalClip => originalClip;

        /// <summary>
        /// 差し替える先のクリップのゲッター、セッター
        /// </summary>
        public AnimationClip OverrideClip
        {
            get { return overrideClip; }
            set { overrideClip = value; }
        }

        /// <summary>
        /// 再生対象ステート名のゲッター、セッター
        /// </summary>
        public string TargetStateName
        {
            get { return targetStateName; }
            set { targetStateName = value; }
        }

        #endregion

    }

}


