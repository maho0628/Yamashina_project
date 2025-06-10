using UnityEngine;
using TMPro;
using DG.Tweening;
using UnityEngine.UI;

public class ComboEffectController : MonoBehaviour, IUIEffectPoolable<ComboEffectController>
{
    [SerializeField] private TextMeshProUGUI comboText;
    private UIObjectPool<ComboEffectController> pool;

    private Color comboEffectColor;


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
        comboEffectColor.a = 1.0f;
        comboText.text = $"Combo: {comboCount}!";
        comboText.color = config.Visual.DisplayColor;
        comboText.alpha = 1f;
        comboText.transform.localScale = Vector3.zero;

        Sequence seq = DOTween.Sequence();
        seq.Append(comboText.transform.DOScale(1f, 0.2f).SetEase(config.Visual.SetScaleEase))
           .AppendInterval(config.Visual.ShowDuration)
           .Append(comboText.DOFade(0f, config.Visual.FadeOutDuration))
           .OnComplete(ReturnToPool);
    }

    public void ReturnToPool()
    {
        DebugManager.Log("ReturnToPool called");

        pool?.Return(this);
        comboEffectColor.a = 0.0f;
        comboText.text = null;


    }
}


