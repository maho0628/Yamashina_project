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
        Debug.Log("OnCreated called on ScoreEffectController"); // ’Ç‰Á

        this.pool = pool;
    }
    private void Start()
    {
        scoreImageColor = scoreText.transform.parent.gameObject.GetComponent<Image>().color;

    }

    public void Play( JudgementConfig config)
    {

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

        Sequence seq = DOTween.Sequence();
        seq.Append(scoreText.transform.DOScale(1f, 0.2f).SetEase(config.Visual.SetScaleEase))
           .AppendInterval(config.Visual.ShowDuration)
           .Append(scoreText.DOFade(0f, config.Visual.FadeOutDuration))
           .OnComplete(ReturnToPool);
    }

    public void ReturnToPool()
    {
        Debug.Log("ReturnToPool called");

        activeSequence?.Kill();
        activeSequence = null;

        pool?.Return(this);
        scoreText.text = null;

        scoreImageColor.a = 0.0f;
      


    }
}
