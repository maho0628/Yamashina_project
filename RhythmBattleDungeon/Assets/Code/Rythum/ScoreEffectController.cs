using UnityEngine;
using TMPro;
using DG.Tweening;
using UnityEngine.UI;

public class ScoreEffectController : MonoBehaviour, IUIEffectPoolable<ScoreEffectController>
{
    [SerializeField] private TextMeshProUGUI scoreText;
    private UIObjectPool<ScoreEffectController> pool;
    private Color scoreImageColor;

    private Sequence activeSequence;

    public void OnCreated(UIObjectPool<ScoreEffectController> pool)
    {

        this.pool = pool;
    }
    private void Start()
    {
        scoreImageColor = scoreText.transform.parent.gameObject.GetComponent<Image>().color;

    }

    public void Play( JudgementConfig config)
    {
        Debug.Log($"[ScoreEffect] Play called: +{config.Logic.SetScoreValue}");

        scoreText.transform.DOKill();
        scoreText.DOKill();
        if (activeSequence != null)
        {
            activeSequence.Kill();
            activeSequence = null;
        }
        scoreText.gameObject.SetActive(true);
        scoreImageColor.a = 1.0f;

        scoreText.text = $"+{config.Logic.SetScoreValue}";
        scoreText.color = config.Visual.DisplayColor;
        scoreText.alpha = 1f;
        scoreText.transform.localScale = Vector3.zero;
        DebugManager.Log($"[ScoreEffect] Playing on instance: {GetInstanceID()}");

        activeSequence = DOTween.Sequence();
        activeSequence.Append(scoreText.transform.DOScale(1f, 0.2f).SetEase(config.Visual.SetScaleEase))
           .AppendInterval(config.Visual.ShowDuration)
           .Append(scoreText.DOFade(0f, config.Visual.FadeOutDuration))
           .OnComplete(ReturnToPool);
    }

    public void ReturnToPool()
    {
        DebugManager.Log("ReturnToPool called");

        activeSequence?.Kill();
        activeSequence = null;

        pool?.Return(this);
        scoreText.text = null;

        scoreImageColor.a = 0.0f;

        scoreText.gameObject.SetActive(false);


    }
}
