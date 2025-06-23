using UnityEngine;
using UnityEngine.UI;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using TMPro;

public class UIManager : SingletonMonoBehaviour<UIManager>
{
    /// <summary>
    /// Re
    /// </summary>
    [Header("プレハブ設定")]
    [SerializeField] private GameObject readyGoPanelPrefab; // プレハブ参照
    [SerializeField] private StartSignalConfig startSignalConfig;

    [Header("親Canvas設定")]
    [SerializeField] private Canvas targetCanvas; // 生成先Canvas

    // 実行時に生成されるインスタンス
    private GameObject readyGoPanelInstance;
    private TextMeshProUGUI readyGoText;



    public async UniTask ShowReadyGoAsync()
    {
        float intervalBetweenReadyGo =startSignalConfig.IntervalBetweenReadyGo; 
        if (targetCanvas == null)
            targetCanvas = GameObject.Find("ReadyGoPanelCanvas").GetComponent<Canvas>();

        // インスタンスが無ければ生成
        if (readyGoPanelInstance == null)
            CreateReadyGoPanel();
        // 親の状態を確認
        readyGoPanelInstance.SetActive(true);

        // Ready演出
        await ShowTextWithConfig(startSignalConfig.ReadyConfig);

        // Ready→Go間の待機時間
        if (intervalBetweenReadyGo > 0)
        {
            await UniTask.Delay(System.TimeSpan.FromSeconds(intervalBetweenReadyGo));
        }

        // Go演出
        await ShowTextWithConfig(startSignalConfig.GoConfig);

        readyGoPanelInstance.SetActive(false);
    }

    private void CreateReadyGoPanel()
    {
        if (readyGoPanelPrefab == null)
        {
            return;
        }

        // プレハブからインスタンス生成
        readyGoPanelInstance = Instantiate(readyGoPanelPrefab, targetCanvas.transform);

        // Text コンポーネントを取得
        readyGoText = readyGoPanelInstance.GetComponentInChildren<TextMeshProUGUI>();

        if (readyGoText == null)
        {
            DebugManager.LogError("ReadyGoPanel内にTextMeshProUGUIコンポーネントが見つかりません！");
        }

        // 初期状態は非表示
        readyGoPanelInstance.SetActive(false);
    }

    private async UniTask ShowTextWithConfig(TextAnimationConfig config)
    {
        if (readyGoText == null) return;

        var basicSettings =config.BasicSettings;
        // テキスト設定
        readyGoText.text = basicSettings.AnimationText;
        readyGoText.fontSize = basicSettings.FontSize;
        readyGoText.font = basicSettings.FontAsset;
        readyGoText.fontStyle = basicSettings.AnimationFontStyles;
        readyGoText.alignment = config.LayoutSettings.Alignment;
        readyGoText.textWrappingMode = config.LayoutSettings.AnimationTextWrappingModes;
        Color animationColor = basicSettings.TextColor;

        animationColor.a = 0.0f;

        readyGoText.color = animationColor;
        var rect = readyGoText.GetComponent<RectTransform>();

        rect.anchorMin = config.LayoutSettings.AnchorMin;
        rect.anchorMax = config.LayoutSettings.AnchorMax;
       
        readyGoText.transform.localScale = Vector3.one * config.ScaleSettings.InitialScale;

        // 演出タイプによって分岐
        switch (basicSettings.AnimationType)
        {
            case AnimationType.Simple:
                await PlaySimpleAnimation(config);
                break;
            case AnimationType.Punch:
                await PlayPunchAnimation(config);
                break;
            case AnimationType.Bounce:
                await PlayBounceAnimation(config);
                break;
            case AnimationType.Custom:
                await PlayCustomAnimation(config);
                break;
        }
    }

    private async UniTask PlaySimpleAnimation(TextAnimationConfig config)
    {
        TextTimingSettings timingSettings = config.TimingSettings;  
        var sequence = DOTween.Sequence();

        sequence.Append(readyGoText.DOFade(timingSettings.FadeInAlpha, timingSettings.FadeInDuration));
        sequence.AppendInterval(timingSettings.DisplayDuration);
        sequence.Append(readyGoText.DOFade(timingSettings.FadeOutAlpha, timingSettings.FadeOutDuration));

        await sequence.AsyncWaitForCompletion();
    }

    private async UniTask PlayPunchAnimation(TextAnimationConfig config)
    {
        TextTimingSettings timingSettings = config.TimingSettings;
        TextPunchSettings punchSettings = config.PunchSettings; 
        var sequence = DOTween.Sequence();

        sequence.Append(readyGoText.DOFade(timingSettings.FadeInAlpha, timingSettings.FadeInDuration))
               .Join(readyGoText.transform.DOPunchScale(punchSettings.PunchPower, punchSettings.PunchDuration, punchSettings.PunchVibrato)
                     .SetEase(punchSettings.EaseType));

        sequence.AppendInterval(timingSettings.DisplayDuration);
        sequence.Append(readyGoText.DOFade(timingSettings.FadeOutAlpha, timingSettings.FadeOutDuration));

        await sequence.AsyncWaitForCompletion();
    }

    private async UniTask PlayBounceAnimation(TextAnimationConfig config)
    {
        var sequence = DOTween.Sequence();
        TextTimingSettings timingSettings =config.TimingSettings;
        TextPunchSettings punchSettings =config.PunchSettings;
        TextScaleSettings scaleSettings = config.ScaleSettings; 
      
        sequence.Append(readyGoText.DOFade(timingSettings.FadeInAlpha, timingSettings.FadeInDuration))
              .Join(readyGoText.transform.DOScale(scaleSettings.TargetScale, scaleSettings.ScaleDuration)
                    .SetEase(punchSettings.EaseType));
        sequence.AppendInterval(timingSettings.DisplayDuration);
        sequence.Append(readyGoText.DOFade(timingSettings.FadeOutAlpha, timingSettings.FadeOutDuration));

        await sequence.AsyncWaitForCompletion();
    }

    private async UniTask PlayCustomAnimation(TextAnimationConfig config)
    {

        AnimationClip animationClip =config.CustomSettings.CustomAnimationClip;
        if (animationClip != null)
        {
            var animator = readyGoText.GetComponent<Animator>();
            if (animator != null)
            {
                animator.Play(animationClip.name);
                await UniTask.Delay(System.TimeSpan.FromSeconds(animationClip.length));
            }
        }
        else
        {
            await PlaySimpleAnimation(config);
        }
    }

    public void StopReadyGo()
    {
        if (readyGoText != null)
            readyGoText.DOKill();

        if (readyGoPanelInstance != null)
            readyGoPanelInstance.SetActive(false);
    }

    private void OnDestroy()
    {
        StopReadyGo();
    }
}