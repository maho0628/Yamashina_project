using UnityEngine;
using TMPro;
using DG.Tweening;
using UnityEngine.UI;

public class ComboEffectController : MonoBehaviour, IUIEffectPoolable<ComboEffectController>
{
    [SerializeField] private TextMeshProUGUI comboText;
    private UIObjectPool<ComboEffectController> pool;

    private Color comboEffectColor;

    private Sequence activeSequence;

    private void Start()
    {
        comboEffectColor = comboText.transform.parent.gameObject.GetComponent<Image>().color;

    }
    public void OnCreated(UIObjectPool<ComboEffectController> pool)
    {
        this.pool = pool;
    }

    public void Play(JudgementConfig config,int comboCount)
    {
        Debug.Log($"[ScoreEffect] Play called: +{config.Logic.SetScoreValue}");
        comboText.gameObject.SetActive(true);

        comboText.transform.DOKill();
        comboText.DOKill();
        if (activeSequence != null)
        {
            activeSequence.Kill();
            activeSequence = null;
        }
        comboEffectColor.a = 1.0f;
        comboText.text = $"Combo: {comboCount}!";
        Color displayColor = config.Visual.DisplayColor;
        displayColor.a = 1f; 
        comboText.color = displayColor;
        comboText.alpha = 1f; 

        comboText.transform.localScale = Vector3.zero;

        activeSequence = DOTween.Sequence();
        activeSequence.Append(comboText.transform.DOScale(1f, 0.2f).SetEase(config.Visual.SetScaleEase))
           .AppendInterval(config.Visual.ShowDuration)
           .Append(comboText.DOFade(0f, config.Visual.FadeOutDuration))
           .OnComplete(ReturnToPool);
    }

    public void ReturnToPool()
    {
        DebugManager.Log("ReturnToPool called");
        activeSequence?.Kill();
        activeSequence = null;
        pool?.Return(this);
        comboEffectColor.a = 0.0f;
        comboText.text = null;

        comboText.gameObject.SetActive(false);

    }
}


