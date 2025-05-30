using UnityEngine;
using TMPro;
using DG.Tweening;
using UnityEngine.UI;



public class JudgeEffectController : MonoBehaviour, IUIEffectPoolable<JudgeEffectController>
{
    [SerializeField] private TextMeshProUGUI judgeText;
    private UIObjectPool<JudgeEffectController> pool;

    private Color judgeEffectColor;


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
        judgeEffectColor.a = 1.0f;
        judgeText.text = config.Visual.DisplayJudgementName;
        judgeText.color = config.Visual.DisplayColor;
        judgeText.alpha = 1f;
        judgeText.transform.localScale = Vector3.zero;

        Sequence seq = DOTween.Sequence();
        seq.Append(judgeText.transform.DOScale(1f, 0.2f).SetEase(config.Visual.SetScaleEase))
           .AppendInterval(config.Visual.SetShowDuration)
           .Append(judgeText.DOFade(0f, config.Visual.SetFadeOutDuration))
           .OnComplete(ReturnToPool);
    }

    public void ReturnToPool()
    {
        pool?.Return(this);
        judgeEffectColor.a = 0.0f;
        judgeText.text =null;


    }
}

