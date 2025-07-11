using UnityEngine;
using TMPro;
using DG.Tweening;
using UnityEngine.UI;



public class JudgeEffectController : MonoBehaviour, IUIEffectPoolable<JudgeEffectController>
{
    [Header("判定テキスト")]
    [SerializeField, Tooltip("判定表示用のTextMeshProUGUI")]
    private TextMeshProUGUI judgeText;

    [Header("初期化用リセットコンフィグ")]
    [SerializeField, Tooltip("リセット時に使うエフェクトの初期設定")]
    private JudgementEffectConfig resetConfig;

    private UIObjectPool<JudgeEffectController> pool;

    private Sequence activeSequence;
    public void OnCreated(UIObjectPool<JudgeEffectController> pool)
    {
        this.pool = pool;
    }

    public void Play(JudgementConfig config)
    {
        Debug.Log($"[ScoreEffect] Play called: +{config.Logic.SetScoreValue}");
        judgeText.gameObject.SetActive(true);

        judgeText.transform.DOKill();
        judgeText.DOKill();
        if (activeSequence != null)
        {
            activeSequence.Kill();
            activeSequence = null;
        }


        var visual = config.Visual;
        var judgeCfg = visual.JudgementEffect;
        // テキスト設定
        judgeText.text = visual.DisplayJudgementName;
        judgeText.color = visual.DisplayColor;
        judgeText.alpha = judgeCfg.StartAlpha;
        judgeText.transform.localScale = judgeCfg.StartScale;

        // アニメーション開始
        activeSequence = DOTween.Sequence()
            .Append(judgeText.transform.DOScale(judgeCfg.EndScale, visual.ScaleInTime).SetEase(visual.SetScaleEase))
            .AppendInterval(visual.ShowDuration)
            .Append(judgeText.DOFade(judgeCfg.EndAlpha, visual.FadeOutDuration))
            .OnComplete(ReturnToPool);
    }

    public void ReturnToPool()
    {
        DebugManager.Log("ReturnToPool called");

        activeSequence?.Kill();
        activeSequence = null;


        judgeText.text = string.Empty;
        judgeText.alpha = resetConfig.StartAlpha;
        judgeText.transform.localScale = resetConfig.StartScale;
        judgeText.gameObject.SetActive(false);

        pool?.Return(this);


    }
}

