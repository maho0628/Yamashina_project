using UnityEngine;
using TMPro;
using DG.Tweening;
using UnityEngine.UI;

public class ScoreEffectController : MonoBehaviour, IUIEffectPoolable<ScoreEffectController>
{
    [Header("UI参照")]
    [SerializeField] private TextMeshProUGUI scoreText;

    [Header("初期化用リセット設定")]
    [SerializeField] private ScoreEffectConfig resetConfig;
    
    private UIObjectPool<ScoreEffectController> pool;


    private Sequence activeSequence;

    public void OnCreated(UIObjectPool<ScoreEffectController> pool)
    {

        this.pool = pool;
    }
 

    public void Play( JudgementConfig config)
    {
        Debug.Log($"[ScoreEffect] Play called: +{config.Logic.SetScoreValue}");
        scoreText.gameObject.SetActive(true);

        scoreText.transform.DOKill();
        scoreText.DOKill();
        if (activeSequence != null)
        {
            activeSequence.Kill();
            activeSequence = null;
        }
        var visual = config.Visual;
        var scoreCfg = visual.ScoreEffect;


        scoreText.text = string.Format(scoreCfg.ScoreTextFormat, config.Logic.SetScoreValue);
        scoreText.color = visual.DisplayColor;
        scoreText.alpha = scoreCfg.StartAlpha;


        scoreText.transform.localScale = scoreCfg.StartScale;
        DebugManager.Log($"[ScoreEffect] Playing on instance: {GetInstanceID()}");

        activeSequence = DOTween.Sequence();
        activeSequence.Append(scoreText.transform.DOScale(scoreCfg.EndScale, visual.ScaleInTime).SetEase(visual.SetScaleEase))
           .AppendInterval(visual.ShowDuration)
           .Append(scoreText.DOFade(scoreCfg.EndAlpha, visual.FadeOutDuration))
           .OnComplete(ReturnToPool);
    }

    public void ReturnToPool()
    {
        DebugManager.Log("ReturnToPool called");

        activeSequence?.Kill();
        activeSequence = null;

        // リセット処理
        scoreText.text = string.Empty;
        scoreText.alpha = resetConfig.StartAlpha;
        scoreText.transform.localScale = resetConfig.StartScale;
        scoreText.gameObject.SetActive(false);


    }
}
