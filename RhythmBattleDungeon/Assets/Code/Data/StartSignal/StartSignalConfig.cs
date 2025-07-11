using UnityEngine;

/// <summary>
/// ゲームスタート演出のスクリプタブルオブジェクト
/// </summary>
[CreateAssetMenu(fileName = "StartSignalConfig", menuName = "GameConfigs/StartSignalConfig")]
public class StartSignalConfig : ScriptableObject
{
    #region ゲームスタート演出の内部管理用変数

    /// <summary>
    /// Ready演出
    /// </summary>
    [Header("Ready演出")]

    [SerializeField, Tooltip("Ready演出設定")]
    private TextAnimationConfig readyConfig ;

    [Space(15)]

    /// <summary>
    /// Go演出
    /// </summary>
    [Header("Go演出")]

    [SerializeField, Tooltip("Go演出設定")]
    private TextAnimationConfig goConfig;
        
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
    /// ジャンルの理想的な演出時間との差（参考値）"
    /// </summary>
    [SerializeField, Tooltip("ジャンルの理想的な演出時間との差（参考値）")]
    private float deviationFromIdeal;

    [Space(15)]

    /// <summary>
    /// Ready→Go全体の所要時間（参考値）
    /// </summary>
    [SerializeField, Tooltip("Ready→Go全体の所要時間（参考値）")]
    private float totalSystemDuration;

    [Space(15)]

    /// <summary>
    /// ゲームジャンル別推奨時間との比較結果
    /// </summary>
    [SerializeField, Tooltip("ゲームジャンル別推奨時間との比較結果")]
    private string genreRecommendation;

    #endregion


    #region 読み取り専用プロパティ(ゲームスタート演出の内部管理用変数)

    /// <summary>
    /// Ready演出の読み取り専用
    /// </summary>
    internal TextAnimationConfig ReadyConfig => readyConfig;

    /// <summary>
    /// Go演出の読み取り専用
    /// </summary>
    internal TextAnimationConfig GoConfig => goConfig;

    /// <summary>
    /// Ready と Go の間の待機時間の読み取り専用
    /// </summary>
    internal float IntervalBetweenReadyGo => intervalBetweenReadyGo;

    #endregion


    #region ゲッター

    /// <summary>
    ///  Ready→Go全体の所要時間を計算して返す
    /// </summary>
    /// <returns>float</returns>
    private float CalculateTotalSystemDuration()
    {
        return readyConfig.TotalDuration + intervalBetweenReadyGo + goConfig.TotalDuration;
    }

    #endregion

    // エディタでのみ実行される更新処理
    private void OnValidate()
    {
        //Ready.Goの更新を呼び出す
        readyConfig.OnValidate();
        goConfig.OnValidate();

        //Go全体の所要時間を計算
        totalSystemDuration = CalculateTotalSystemDuration();

        // 自動ジャンル判定（Custom以外なら比較）
        if (targetGenre != GameGenre.Custom)
        {
            //そのジャンルの推奨時間を取得
            var ideal = GenreTimeRecommendations.GetRecommendedTime(targetGenre);

            //そのジャンルの理想的な演出時間との差-そのジャンルの推奨時間　=ジャンルの理想的な演出時間との差
            deviationFromIdeal = ideal >= 0 ? totalSystemDuration - ideal : 0f;
        }

        // 自動で一番近いジャンルを推定
        var closestGenre = GenreTimeRecommendations.GetClosestGenre(totalSystemDuration);

        //ゲームジャンル別推奨時間との比較結果を取得
        genreRecommendation = GenreTimeRecommendations.GetLabel(closestGenre);
    }
}