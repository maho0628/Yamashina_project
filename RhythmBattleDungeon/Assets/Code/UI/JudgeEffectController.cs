using UnityEngine;
using TMPro;
using DG.Tweening;
using UnityEngine.UI;



public class JudgeEffectController : MonoBehaviour, IUIEffectPoolable<JudgeEffectController>
{
    [SerializeField] private TextMeshProUGUI judgeText;
    private UIObjectPool<JudgeEffectController> pool;

    private Color judgeEffectColor;

    private Sequence activeSequence;

    private void Start()
    {
        judgeEffectColor = judgeText.transform.parent.gameObject.GetComponent<Image>().color;


    }
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
        judgeEffectColor.a = 1.0f;
        judgeText.text = config.Visual.DisplayJudgementName;
        Color displayColor = config.Visual.DisplayColor;
        displayColor.a = 1f;
        judgeText.color = displayColor;
        judgeText.alpha = 1f;
        judgeText.transform.localScale = Vector3.zero;

        activeSequence = DOTween.Sequence();
        activeSequence.Append(judgeText.transform.DOScale(1f, 0.2f).SetEase(config.Visual.SetScaleEase))
           .AppendInterval(config.Visual.ShowDuration)
           .Append(judgeText.DOFade(0f, config.Visual.FadeOutDuration))
           .OnComplete(ReturnToPool);
    }

    public void ReturnToPool()
    {
        DebugManager.Log("ReturnToPool called");

        activeSequence?.Kill();
        activeSequence = null;

        pool?.Return(this);
        judgeText.text = null;
        judgeText.alpha = 0f; 
        judgeText.transform.localScale = Vector3.zero;

        judgeEffectColor.a = 0.0f;


    }
}

