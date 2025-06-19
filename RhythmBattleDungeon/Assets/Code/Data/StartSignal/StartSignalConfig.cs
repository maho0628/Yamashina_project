using UnityEngine;

/// <summary>
/// ゲームスタート演出に関するスクリプタブルオブジェクト
/// </summary>
[CreateAssetMenu(fileName = "StartSignalConfig", menuName = "GameConfigs/StartSignalConfig")]
public class StartSignalConfig : ScriptableObject
{
    #region ゲームスタート演出

    /// <summary>
    /// Ready演出
    /// </summary>
    [Header("Ready演出")]
    [SerializeField, Tooltip("Ready演出設定")]
    private TextAnimationConfig readyConfig = new TextAnimationConfig
    {
        AnimationText = "Ready",
        textColor = Color.white,
        animationType = AnimationType.Simple
    };

    [Space(15)]

    /// <summary>
    /// Go演出
    /// </summary>
    [Header("Go演出")]
    [SerializeField, Tooltip("Go演出設定")]
    private TextAnimationConfig goConfig = new TextAnimationConfig
    {
        AnimationText = "Go!",
        textColor = Color.red,
        animationType = AnimationType.Punch
    };

    [Space(15)]

    /// <summary>
    /// Ready と Go の間の待機時間
    /// </summary>
    [Header("全体設定")]
    [SerializeField, Tooltip("Ready と Go の間の待機時間")]
    private float intervalBetweenReadyGo = 0.2f;

    [Space(15)]

    /// <summary>
    /// ターゲットとするジャンル
    /// </summary>
    [Header("ジャンル設定")]
    [SerializeField, Tooltip("ターゲットとするジャンル")]
    private GameGenre targetGenre = GameGenre.Custom;

    [Space(15)]

    /// <summary>
    /// ターゲットとするジャンル
    /// </summary>
    [SerializeField, Tooltip("ジャンルの理想的な演出時間との差（参考値）")]
    private float deviationFromIdeal;

    [Space(15)]

    /// <summary>
    /// ターゲットとするジャンル
    /// </summary>
    [SerializeField, Tooltip("Ready→Go全体の所要時間（参考値）")]
    private float totalSystemDuration;

    [Space(15)]

    [SerializeField, Tooltip("ゲームジャンル別推奨時間との比較")]
    private string genreRecommendation;

    #endregion

    internal float TotalDuration =>
        readyConfig.TotalDuration + intervalBetweenReadyGo + goConfig.TotalDuration;

    internal float IntervalBetweenReadyGo => intervalBetweenReadyGo;

    internal TextAnimationConfig ReadyConfig => readyConfig;

    internal TextAnimationConfig GoConfig => goConfig;

    // エディタでのみ実行される更新処理
    private void OnValidate()
    {
        readyConfig.OnValidate();
        goConfig.OnValidate();

        totalSystemDuration = TotalDuration;

        // 自動ジャンル判定（Custom以外なら比較）
        if (targetGenre != GameGenre.Custom)
        {
            var ideal = GenreTimeRecommendations.GetRecommendedTime(targetGenre);
            deviationFromIdeal = ideal >= 0 ? totalSystemDuration - ideal : 0f;
        }

        // 自動で一番近いジャンルを推定
        var closestGenre = GenreTimeRecommendations.GetClosestGenre(totalSystemDuration);
        genreRecommendation = GenreTimeRecommendations.GetLabel(closestGenre);
    }
}