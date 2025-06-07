using UnityEngine;
using UnityEngine.UI;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using TMPro;

public class UIManager : SingletonMonoBehaviour<UIManager>
{
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
        if (targetCanvas == null)
            targetCanvas = GameObject.Find("ReadyGoPanelCanvas").GetComponent<Canvas>();

        // インスタンスが無ければ生成
        if (readyGoPanelInstance == null)
            CreateReadyGoPanel();
        // 親の状態を確認
        readyGoPanelInstance.SetActive(true);

        // Ready演出
        await ShowTextWithConfig(startSignalConfig.readyConfig);

        // Ready→Go間の待機時間
        if (startSignalConfig.intervalBetweenReadyGo > 0)
        {
            await UniTask.Delay(System.TimeSpan.FromSeconds(startSignalConfig.intervalBetweenReadyGo));
        }

        // Go演出
        await ShowTextWithConfig(startSignalConfig.goConfig);

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

        // テキスト設定
        readyGoText.text = config.AnimationText;
        readyGoText.color = config.textColor;
        readyGoText.fontSize = config.fontSize;

        // 初期状態設定
        readyGoText.color = new Color(config.textColor.r, config.textColor.g, config.textColor.b, 0f);
        readyGoText.transform.localScale = Vector3.one * config.initialScale;

        // 演出タイプによって分岐
        switch (config.animationType)
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
        var sequence = DOTween.Sequence();

        sequence.Append(readyGoText.DOFade(1f, config.fadeInDuration));
        sequence.AppendInterval(config.displayDuration);
        sequence.Append(readyGoText.DOFade(0f, config.fadeOutDuration));

        await sequence.AsyncWaitForCompletion();
    }

    private async UniTask PlayPunchAnimation(TextAnimationConfig config)
    {
        var sequence = DOTween.Sequence();

        sequence.Append(readyGoText.DOFade(1f, config.fadeInDuration))
               .Join(readyGoText.transform.DOPunchScale(config.punchPower, config.punchDuration, config.punchVibrato)
                     .SetEase(config.easeType));

        sequence.AppendInterval(config.displayDuration);
        sequence.Append(readyGoText.DOFade(0f, config.fadeOutDuration));

        await sequence.AsyncWaitForCompletion();
    }

    private async UniTask PlayBounceAnimation(TextAnimationConfig config)
    {
        var sequence = DOTween.Sequence();

        sequence.Append(readyGoText.DOFade(1f, config.fadeInDuration))
               .Join(readyGoText.transform.DOScale(config.targetScale, config.scaleDuration)
                     .SetEase(config.easeType));

        sequence.AppendInterval(config.displayDuration);
        sequence.Append(readyGoText.DOFade(0f, config.fadeOutDuration));

        await sequence.AsyncWaitForCompletion();
    }

    private async UniTask PlayCustomAnimation(TextAnimationConfig config)
    {
        if (config.customAnimationClip != null)
        {
            var animator = readyGoText.GetComponent<Animator>();
            if (animator != null)
            {
                animator.Play(config.customAnimationClip.name);
                await UniTask.Delay(System.TimeSpan.FromSeconds(config.customAnimationClip.length));
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