using UnityEditor;
using UnityEngine;

[CreateAssetMenu(menuName = "TextAnimation/Config")]

/// <summary>
/// テキストのアニメーションに関するデータを総合的に持つ
/// </summary>
public class TextAnimationConfig : ScriptableObject
{
    #region テキストアニメーションの内部管理用変数

    /// <summary>
    /// アニメーションの基本設定のデータ
    /// </summary>
    [Header("アニメーションの基本設定")]
    [SerializeField, Tooltip("フォントサイズやカラーなど、基本的な表示設定を指定します。")]
    private TextBasicSettings basicSettings;

    [Space(15)]

    /// <summary>
    /// アニメーションのタイミング設定のデータ
    /// </summary>
    [Header("アニメーションのタイミング設定")]
    [SerializeField, Tooltip("フェードイン・表示・フェードアウトの時間などを設定します。")]
    private TextTimingSettings timingSettings;

    [Space(15)]

    /// <summary>
    /// アニメーションのスケール設定のデータ
    /// </summary>
    [Header("アニメーションのスケール設定")]
    [SerializeField, Tooltip("拡大・縮小アニメーションの詳細を設定します。")]
    private TextScaleSettings scaleSettings;

    [Space(15)]

    /// <summary>
    /// アニメーションのパンチアニメ設定のデータ
    /// </summary>
    [Header("アニメーションのパンチアニメ設定")]
    [SerializeField, Tooltip("パンチ（跳ねるような）アニメーションの設定を行います。")]
    private TextPunchSettings punchSettings;

    [Space(15)]

    /// <summary>
    /// カスタムアニメーション設定
    /// </summary>
    [Header("カスタムアニメーション設定")]
    [SerializeField, Tooltip("独自に定義されたアニメーション挙動を設定します。")]
    private TextCustomSettings customSettings;

    [Space(15)]

    /// <summary>
    /// レイアウト＆Canvas設定
    /// </summary>
    [Header("レイアウト＆Canvas設定")]
    [SerializeField, Tooltip("表示位置や親Canvasの設定など、レイアウトに関する設定です。")]
    private TextLayoutSettings layoutSettings;

    [Space(15)]

    /// <summary>
    /// アニメーション時間のバリデーション設定
    /// </summary>
    [Header("アニメーション時間のバリデーション設定")]
    [SerializeField, Tooltip("アニメーションの長さに対する警告の基準値です")]
    private TextValidationSettings validationSettings;

    [Space(15)]

    /// <summary>
    /// タイミング設定に基づいた合計アニメーション時間（秒単位）です。自動計算されます。
    /// </summary>
    [Header("参考情報")]
    [SerializeField, Tooltip("タイミング設定に基づいた合計アニメーション時間（秒単位）です。自動計算されます。")]
    private float totalDuration;

    [Space(15)]

    /// <summary>
    /// アニメーション時間の適正さに関するチェック結果です。自動的に更新されます。
    /// </summary>
    [SerializeField, Tooltip("アニメーション時間の適正さに関するチェック結果です。自動的に更新されます。")]
    private string durationCheck;

    #endregion


    #region 読み取り専用プロパティ(テキストアニメーションの内部管理用変数)

    /// <summary>
    /// アニメーションの基本設定のデータの読み取り専用
    /// </summary>
    internal TextBasicSettings BasicSettings
    {
        get { return basicSettings; }
        set { basicSettings = value; }
    }
    /// <summary>
    /// アニメーションのタイミング設定のデータの読み取り専用
    /// </summary>
    internal TextTimingSettings TimingSettings
    {
        get { return timingSettings; }
        set { timingSettings = value; }
    }

    /// <summary>
    /// アニメーションのスケール設定のデータの読み取り専用
    /// </summary>
    internal TextScaleSettings ScaleSettings
    {
        get { return scaleSettings; }
        set { scaleSettings = value; }
    }

    /// <summary>
    /// アニメーションのパンチアニメ設定のデータの読み取り専用
    /// </summary>
    internal TextPunchSettings PunchSettings
    {
        get { return punchSettings; }
        set { punchSettings = value; }
    }

    /// <summary>
    /// カスタムアニメーション設定のデータの読み取り専用
    /// </summary>
    internal TextCustomSettings CustomSettings
    {
        get { return customSettings; }
        set { customSettings = value; }
    }

    /// <summary>
    /// レイアウト＆Canvas設定のデータの読み取り専用
    /// </summary>
    internal TextLayoutSettings LayoutSettings
    {
        get { return layoutSettings; }
        set { layoutSettings = value; }
    }

    /// <summary>
    /// タイミング設定に基づいた合計アニメーション時間の読み取り専用
    /// </summary>
    internal float TotalDuration => timingSettings.FadeInDuration + timingSettings.DisplayDuration + timingSettings.FadeOutDuration;


    #endregion


    /// <summary>
    /// 外部で呼び出すValidate関数
    /// </summary>
    internal void OnValidate()
    {
        //タイミング設定に基づいた合計アニメーション時間を反映
        totalDuration = TotalDuration;

        //アニメーション時間のバリデーション設定にデータが入っているかチェック
        if (validationSettings == null)
        {
            //未割り当てを伝えて早期終了
            durationCheck = "⚠️ Validation設定が未割り当てです";
            return;
        }

        //タイミング設定に基づいた合計アニメーション時間がアニメーション時間の妥当性チェック設定より短ければ
        if (totalDuration < validationSettings.MinDurationThreshold)

            durationCheck = $"⚠️ 短すぎる（{validationSettings.MinDurationThreshold}秒未満）";

        //タイミング設定に基づいた合計アニメーション時間がアニメーション時間の妥当性チェック設定より長ければ
        else if (totalDuration > validationSettings.MaxDurationThreshold)

            durationCheck = $"⚠️ 長すぎる（{validationSettings.MaxDurationThreshold}秒超過）";

        //適切だと表示
        else
            durationCheck = "✅ 適切な長さ";
    }

}
