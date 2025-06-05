using DG.Tweening;
using UnityEngine;

[System.Serializable]
public class TextAnimationConfig
{
    [Header("基本設定")]
    public string text = "Ready";
    public Color textColor = Color.white;
    public int fontSize = 48;

    [Header("演出タイプ")]
    public AnimationType animationType = AnimationType.Simple;

    [Header("タイミング設定")]
    public float fadeInDuration = 0.3f;
    public float displayDuration = 1.0f;
    public float fadeOutDuration = 0.3f;

    [Header("スケール設定")]
    public float initialScale = 0.5f;
    public float targetScale = 1.0f;
    public float scaleDuration = 0.5f;

    [Header("パンチ設定（Punch選択時のみ）")]
    public Vector3 punchPower = new Vector3(0.2f, 0.2f, 0f);
    public float punchDuration = 0.6f;
    public int punchVibrato = 3;

    [Header("イージング")]
    public Ease easeType = Ease.OutBack;

    [Header("カスタムアニメーション（Custom選択時のみ）")]
    public AnimationClip customAnimationClip;

    [Header("参考情報")]
    [Tooltip("この演出の合計時間（参考値）")]
    [SerializeField] private float totalDuration;

    [Tooltip("推奨時間範囲に収まっているか")]
    [SerializeField] private string durationCheck;
    public float TotalDuration => fadeInDuration + displayDuration + fadeOutDuration;

    // エディタでのみ実行される更新処理
    internal void OnValidate()
    {
        totalDuration = TotalDuration;

        // 時間チェック
        if (totalDuration < 0.5f)
            durationCheck = "⚠️ 短すぎる（0.5秒未満）";
        else if (totalDuration > 3.0f)
            durationCheck = "⚠️ 長すぎる（3秒超過）";
        else
            durationCheck = "✅ 適切な長さ";
    }
}

public enum AnimationType
{
    Simple,     // シンプルなフェード
    Punch,      // パンチエフェクト
    Bounce,     // バウンススケール
    Custom      // カスタムアニメーション
}