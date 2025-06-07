using UnityEngine;
using UnityEngine.UI;
using Cysharp.Threading.Tasks;

public class ScoreGaugeUI : MonoBehaviour
{
    [SerializeField, Header("ÉQÅ[ÉWUIñ{ëÃ")]
    private Image gaugeImage;

    [SerializeField, Header("ÉQÅ[ÉWîwåiâÊëú")]
    private Image backgroundImage;

    private float currentFill = 0f;
    private float targetFill = 0f;
    private bool isAnimating = false;

    private GaugeConfig config;

    private void Start()
    {
        config = GaugeManager.Instance.GetCurrentConfig();

        if (backgroundImage != null)
        {
            backgroundImage.color = config.Visual.GaugeBackgroundColor;
        }
        currentFill = config.Debug.DebugInitialValue;
        gaugeImage.fillAmount = currentFill;
        gaugeImage.color = config.Visual.GaugeFillColor;

        ScoreManager.Instance.OnScoreChanged += OnScoreChanged;
    }

    private void OnDestroy()
    {
        if (ScoreManager.Instance)
        {
            ScoreManager.Instance.OnScoreChanged -= OnScoreChanged;
        }
    }

    private void OnScoreChanged(int newScore)
    {
        float maxScore = ScoreManager.Instance.GetMaxScore();

        if (maxScore == 0)
        {
            return;
        }

        targetFill = Mathf.Clamp01((float)newScore / maxScore);
        DebugManager.Log($"[ScoreGauge] targetFill: {targetFill}");
        AnimateGaugeAsync().Forget();
    }
    public void ResetGauge()
    {

        if (backgroundImage != null)
        {
            backgroundImage.color = config.Visual.GaugeBackgroundColor;
        }

        currentFill = config.Debug.DebugInitialValue;
        targetFill = config.Debug.DebugInitialValue;

        gaugeImage.fillAmount = currentFill;
        gaugeImage.color = config.Visual.GaugeFillColor;

        isAnimating = false;
    }
    private async UniTask AnimateGaugeAsync()
    {
        if (isAnimating && !config.Debug.DebugAlwaysAnimate) return;

        isAnimating = true;
        float time = 0f;
        float startFill = currentFill;
        float duration = config.Animation.GaugeLerpDuration;

        while (time < duration)
        {
            time += Time.deltaTime;
            float t = Mathf.Clamp01(time / duration);
            float evaluatedT = config.Animation.UseEasing ? config.Animation.GaugeAnimationCurve.Evaluate(t) : t;
            currentFill = Mathf.Lerp(startFill, targetFill, evaluatedT);
            gaugeImage.fillAmount = currentFill;
            await UniTask.Yield();
        }

        currentFill = targetFill;
        gaugeImage.fillAmount = targetFill;
        isAnimating = false;

        if (!Mathf.Approximately(currentFill, targetFill))
        {
            await AnimateGaugeAsync();
        }
    }
}
