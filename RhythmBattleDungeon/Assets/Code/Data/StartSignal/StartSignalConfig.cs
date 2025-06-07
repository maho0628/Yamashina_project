using UnityEngine;


[CreateAssetMenu(fileName = "StartSignalConfig", menuName = "GameConfigs/StartSignalConfig")]
public class StartSignalConfig : ScriptableObject
{
    [Header("Ready演出")]
    public TextAnimationConfig readyConfig = new TextAnimationConfig
    {
        AnimationText = "Ready",
        textColor = Color.white,
        animationType = AnimationType.Simple
    };

    [Header("Go演出")]
    public TextAnimationConfig goConfig = new TextAnimationConfig
    {
        AnimationText = "Go!",
        textColor = Color.red,
        animationType = AnimationType.Punch
    };

    [Header("全体設定")]
    [Tooltip("Ready と Go の間の待機時間")]
    public float intervalBetweenReadyGo = 0.2f;

    [Header("参考情報")]
    [Tooltip("Ready→Go全体の所要時間（参考値）")]
    [SerializeField] private float totalSystemDuration;

    [Tooltip("ゲームジャンル別推奨時間との比較")]
    [SerializeField] private string genreRecommendation;
    public float TotalDuration =>
        readyConfig.TotalDuration + intervalBetweenReadyGo + goConfig.TotalDuration;

    // エディタでのみ実行される更新処理
    private void OnValidate()
    {
        // 個別の演出チェック
        readyConfig.OnValidate();
        goConfig.OnValidate();

        // 全体時間更新
        totalSystemDuration = TotalDuration;

        // ジャンル別推奨時間チェック
        if (totalSystemDuration < 1.0f)
            genreRecommendation = " 超高速（格闘ゲーム向け）";
        else if (totalSystemDuration < 1.5f)
            genreRecommendation = " 高速（アクション向け）";
        else if (totalSystemDuration < 2.5f)
            genreRecommendation = " 標準（一般的）";
        else if (totalSystemDuration < 4.0f)
            genreRecommendation = " ドラマチック（RPG向け）";
        else
            genreRecommendation = "長すぎる（要調整）";
    }
}