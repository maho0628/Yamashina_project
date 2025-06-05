using UnityEngine;

public class AnimationManager : SingletonMonoBehaviour<AnimationManager>
{
    private UIObjectPool<JudgeEffectController> judgeEffectPool;
    private UIObjectPool<ScoreEffectController> scoreEffectPool;
private UIObjectPool<ComboEffectController> comboEffectPool;   
    private Animator titleAnimator;
    private Animator resultAnimator;

    protected override void Awake()
    {
        base.Awake();
        InitEffectController();
    }
    

    public void InitTitleAnimator(Animator animator)
    {
        titleAnimator = animator;
    }
    public void InitResultAnimator(Animator animator)
    {
        resultAnimator = animator;
    }
    public void InitEffectController()
    {
        judgeEffectPool = FindAnyObjectByType<UIObjectPool<JudgeEffectController>>();
        scoreEffectPool = FindAnyObjectByType<UIObjectPool<ScoreEffectController>>();
        comboEffectPool = FindAnyObjectByType<UIObjectPool<ComboEffectController>>();
    }



    public void ShowJudgeEffect(JudgementConfig config)
    {
        if (judgeEffectPool == null)
        {
            Debug.LogWarning("[AnimationManager] judgeEffectPool Ç™ñ¢ê›íË");
            return;
        }

        var ctrl = judgeEffectPool.Get();
        ctrl.Play(config);
    }
    public void ShowComboEffect(JudgementConfig config)
    {
        if (comboEffectPool == null)
        {
            Debug.LogWarning("[AnimationManager] judgeEffectPool Ç™ñ¢ê›íË");
            return;
        }

        var ctrl = comboEffectPool.Get();
        ctrl.Play(config,ComboManager.Instance.CurrentCombo );
    }
    public void ShowScoreEffect(JudgementConfig config)
    {
        if (scoreEffectPool == null)
        {
            Debug.LogWarning("[AnimationManager] judgeEffectPool Ç™ñ¢ê›íË");
            return;
        }

        var ctrl = scoreEffectPool.Get();
        ctrl.Play(config);
    }


    public void PlayTitleIntro()
    {
        if (titleAnimator == null) return;
        titleAnimator.SetTrigger("Intro");
    }

    public void PlayResultInAnimation()
    {
        if (resultAnimator == null) return;
        resultAnimator.SetTrigger("Idle");
    }
}
