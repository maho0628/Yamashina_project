using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using Cysharp.Threading.Tasks;
using System.Linq;

/// <summary>
/// テキストアニメーションの共通処理を提供する静的クラス
/// UIManagerとTextAnimationPreviewerで共有使用
/// </summary>
public static class TextAnimationPlayer
{
    /// <summary>
    /// テキストに設定を適用してアニメーションを再生する
    /// </summary>
    /// <param name="textComponent">対象のTextMeshProUGUIコンポーネント</param>
    /// <param name="config">アニメーション設定</param>
    /// <param name="isPreview">プレビューモードかどうか（UniTask/Task切り替え用）</param>
    internal static async UniTask PlayTextAnimationAsync(TextMeshProUGUI textComponent, TextAnimationConfig config, bool isPreview = false)
    {
        if (textComponent == null || config == null) return;

        // テキストに設定を適用
        ApplyTextConfiguration(textComponent, config);
        DebugManager.Log(config.Params.Basic.AnimationType.ToString());

        // アニメーションを再生
        await PlayAnimationByType(textComponent, config, isPreview);
    }

    /// <summary>
    /// アニメーション設定から各パラメータを取得する
    /// </summary>
    /// <param name="config">アニメーション設定</param>
    /// <returns>取得した各設定のタプル</returns>
    private static (
        TextBasicSettings Basic,
        TextLayoutSettings Layout,
        TextTimingSettings Timing,
        TextScaleSettings Scale,
        TextPunchSettings Punch,
        TextCustomSettings Custom
    ) GetAnimationParameters(TextAnimationConfig config)
    {
        var animationParams = config.Params;
        return (
             animationParams.Basic,
             animationParams.Layout,
             animationParams.Timing,
             animationParams.Scale,
             animationParams.Punch,
             animationParams.Custom
        );
    }
    /// <summary>
    /// テキストコンポーネントに設定を適用
    /// </summary>
    /// <param name="textComponent">対象のTextMeshProUGUIコンポーネント</param>
    /// <param name="config">アニメーション設定</param>
    private static void ApplyTextConfiguration(TextMeshProUGUI textComponent, TextAnimationConfig config)
    {
        //アニメーションの各設定を取得
        var (basicSettings, layoutSettings, timing, scale, _, _) = GetAnimationParameters(config);

        // 基本テキスト設定を反映
        textComponent.text = basicSettings.AnimationText;//表示する文字テキスト
        textComponent.fontSize = basicSettings.FontSize;//フォントサイズ
        textComponent.font = basicSettings.FontAsset;//フォントアセット
        textComponent.fontStyle = basicSettings.AnimationFontStyles;//フォントスタイル（太字、斜体など）
        textComponent.alignment = layoutSettings.Alignment;//文字揃え設定（左揃え、中央揃え、右揃えなど）
        textComponent.textWrappingMode = layoutSettings.AnimationTextWrappingModes;// 折り返し設定

        // 背景画像設定（親のImageコンポーネントがあれば）
        var parentImage = textComponent.GetComponentInParent<Image>();
        if (parentImage != null && basicSettings.BackGroundImage != null)
        {
            parentImage.sprite = basicSettings.BackGroundImage;
            parentImage.rectTransform.sizeDelta = new Vector2(layoutSettings.TextBoxWidth, layoutSettings.TextBoxHeight);
        }

        // 初期カラー設定（透明度調整）
        var color = basicSettings.TextColor;
        color.a = timing.FadeInAlpha;
        textComponent.color = color;

        // アンカー位置設定
        var rectTransform = textComponent.rectTransform;
        rectTransform.anchorMin = layoutSettings.AnchorMin;
        rectTransform.anchorMax = layoutSettings.AnchorMax;

        // 初期スケール設定
        textComponent.transform.localScale = Vector3.one * scale.InitialScale;
    }

    /// <summary>
    /// アニメーションタイプに応じてアニメーションを再生
    /// </summary>
    /// <param name="textComponent">対象のTextMeshProUGUIコンポーネント</param>
    /// <param name="config">アニメーション設定</param>
    /// <param name="isPreview">プレビューモードかどうか</param>
    /// <returns></returns>
    private static async UniTask PlayAnimationByType(TextMeshProUGUI textComponent, TextAnimationConfig config, bool isPreview)
    {
        //アニメーションの各設定を取得
        var (basicSettings, _, timing, scale, punch, _) = GetAnimationParameters(config);

        //アニメーションタイプに応じてアニメーションを再生
        switch (basicSettings.AnimationType)
        {
            // シンプルなフェードイン・表示・フェードアウトアニメーション

            case AnimationType.Simple:
                await PlayWithCommonFade(textComponent, timing, isPreview);
                break;

            // 「パンチ」アニメーション（スケールを一時的に変化させる）と共通のフェードイン・フェードアウトを組み合わせて再生
            case AnimationType.Punch:
                await PlayWithCommonFade(textComponent, timing, isPreview,
                    textComponent.transform.DOPunchScale(punch.PunchPower, punch.PunchDuration, punch.PunchVibrato)
                        .SetEase(punch.EaseType));
                break;

            // スケールアップ・ダウンを行うバウンド演出
            case AnimationType.Bounce:
                await PlayWithCommonFade(textComponent, timing, isPreview,
                    textComponent.transform.DOScale(scale.TargetScale, scale.ScaleDuration)
                        .SetEase(punch.EaseType));
                break;

            // カスタムアニメーション（AnimatorのAnimationClip再生）
            case AnimationType.Custom:
                await PlayCustomAnimation(textComponent, config, isPreview);
                break;

        }

    }

    /// <summary>
    /// 共通のフェードアニメーション処理
    /// </summary>
    private static async UniTask PlayWithCommonFade(TextMeshProUGUI textComponent, TextTimingSettings timing, bool isPreview, Tween customTween = null)
    {
        var sequence = DOTween.Sequence();

        // フェードイン
        sequence.Append(textComponent.DOFade(timing.FadeInAlpha, timing.FadeInDuration));

        // カスタムTweenがある場合は同時実行
        if (customTween != null)
            sequence.Join(customTween);

        // 表示時間
        sequence.AppendInterval(timing.DisplayDuration);

        // フェードアウト
        sequence.Append(textComponent.DOFade(timing.FadeOutAlpha, timing.FadeOutDuration));

        // 完了待機
        if (isPreview)
        {
            // プレビュー用：単純な時間待機
            var totalDuration = timing.FadeInDuration + timing.DisplayDuration + timing.FadeOutDuration;
            await UniTask.Delay(System.TimeSpan.FromSeconds(totalDuration));
        }
        else
        {
            // 通常用：DOTweenの完了待機
            await sequence.AsyncWaitForCompletion();
        }
    }

    /// <summary>
    /// カスタムアニメーション処理
    /// </summary>
    private static async UniTask PlayCustomAnimation(TextMeshProUGUI textComponent, TextAnimationConfig config, bool isPreview)
    {
        //アニメーションの各設定を取得
        var (_, _, timing, _, _, customSettings) = GetAnimationParameters(config);

        //カスタム設定の中からアニメーションクリップを取得
        var overridePairs = customSettings.OverridePairs;

        //カスタム設定の中からアニメーションコントローラーを取得
        var baseController = customSettings.BaseAnimatorController;

        //クリップとコントローラーがない場合
        if (overridePairs == null || baseController == null)
        {
            //エラー出して処理しない
            DebugManager.LogError("CustomAnimationClip または BaseAnimatorController が設定されていません");
            return;
        }

        // TMPの親のアニメーターを取得
        var animatorTarget = textComponent.transform.parent.gameObject;
           var animator = animatorTarget.GetComponent<Animator>() ?? animatorTarget.AddComponent<Animator>();

        //まだアニメータは動かしたくないので無効化
        animator.enabled = false;

        // OverrideController を作成し、クリップを差し替え
        var overrideController = new AnimatorOverrideController(baseController);
        foreach (var pair in overridePairs)
        {
            if (!overrideController.animationClips.Contains(pair.OriginalClip))
            {
                DebugManager.LogWarning($"{pair.OriginalClip.name} は AnimatorController に含まれていません");
                continue;
            }
            if (pair.OriginalClip != null && pair.OverrideClip != null)
            {
                overrideController[pair.OriginalClip] = pair.OverrideClip;
            }
            animator.runtimeAnimatorController = overrideController;

            if (animator != null)
            {

                animator.enabled = true;

                var playStateName = overridePairs.FirstOrDefault()?.TargetStateName ?? "Default";
                animator.Play(playStateName);
                await UniTask.Delay(System.TimeSpan.FromSeconds(pair.OverrideClip.length));
                return;
            }
        }



        // フォールバック：通常のフェードアニメーション
        await PlayWithCommonFade(textComponent, timing, isPreview);
    }

    /// <summary>
    /// アニメーションを強制停止
    /// </summary>
    internal static void StopAnimation(TextMeshProUGUI textComponent)
    {
        if (textComponent != null)
        {
            textComponent.DOKill();
        }
    }
}