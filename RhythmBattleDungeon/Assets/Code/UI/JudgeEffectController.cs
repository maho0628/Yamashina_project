using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class JudgeEffectController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI judgeText;

    private Tween currentTween;

    public void Play(JudgementConfig config)
    {
        if (currentTween != null && currentTween.IsActive())
        {
            currentTween.Kill();
        }

        judgeText.text = config.Visual.SetDisplayJudgementName;
        judgeText.color = config.Visual.SetDisplayColor;
        judgeText.alpha = 1f;
        judgeText.transform.localScale = Vector3.zero;

        // 拡大 → 少し待って → フェードアウト
        Sequence seq = DOTween.Sequence();
        seq.Append(judgeText.transform.DOScale(1f, 0.2f).SetEase(Ease.OutBack))
           .AppendInterval(config.Visual.SetShowDuration)
           .Append(judgeText.DOFade(0f, config.Visual.SetFadeOutDuration));

        currentTween = seq;
    }
}
