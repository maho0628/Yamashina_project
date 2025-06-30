using UnityEditor;
using UnityEngine;

[CreateAssetMenu(menuName = "TextAnimation/Config")]

/// <summary>
/// テキストのアニメーションのデータを総合的に持つ
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
    [SerializeField, Tooltip("表示位置や親Canvasの設定など、レイアウトの設定です。")]
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
    [SerializeField, Tooltip("アニメーション時間の適正さに関するチェック結果です。\n自動的に更新されます。")]
    private string durationCheck;

    #endregion


    #region 読み取り専用プロパティ(テキストアニメーションの内部管理用変数)

    /// <summary>
    /// タイミング設定に基づいた合計アニメーション時間の読み取り専用
    /// </summary>
    internal float TotalDuration => timingSettings.FadeInDuration + timingSettings.DisplayDuration + timingSettings.FadeOutDuration;

    /// <summary>
    /// この設定オブジェクトからすべてのアニメーション関連設定をまとめた構造体を取得します。
    /// </summary>
    internal TextAnimationParams Params => new TextAnimationParams(this);


    #endregion

    /// <summary>
    /// TextAnimationConfig の各設定項目をまとめて取得するための構造体。
    /// 読み取り専用で、設定値への簡潔なアクセスを提供する。
    /// </summary>
    internal readonly struct TextAnimationParams
    {
        internal TextTimingSettings Timing { get; }
        internal TextPunchSettings Punch { get; }
        internal TextScaleSettings Scale { get; }
        internal TextBasicSettings Basic { get; }
        internal TextCustomSettings Custom { get; }
        internal TextLayoutSettings Layout { get; }

        /// <summary>
        /// 指定されたアニメーション設定から各カテゴリの設定値を抽出して初期化します。
        /// </summary>
        /// <param name="config">元となるテキストアニメーション設定</param>
        internal TextAnimationParams(TextAnimationConfig config)
        {
            Timing = config.timingSettings;
            Punch = config.punchSettings;
            Scale = config.scaleSettings;
            Basic = config.basicSettings;
            Custom = config.customSettings;
            Layout = config.layoutSettings;
        }
    }

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
            //未割り当てを伝えて処理しない
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
