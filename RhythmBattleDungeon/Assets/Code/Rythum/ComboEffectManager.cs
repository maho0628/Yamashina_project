using UnityEngine;
using TMPro;
using DG.Tweening;
using UnityEngine.UI;

/// <summary>
/// コンボ演出を制御するクラス。
/// オブジェクトプールによって管理され、スコアに応じたエフェクトを再生します。
/// </summary>
public class ComboEffectController : MonoBehaviour, IUIEffectPoolable<ComboEffectController>
{
    #region コンボ演出を制御するために必要な内部情報処理変数

    /// <summary>
    /// コンボ回数を表示するTextMeshProのUI要素
    /// </summary>
    [SerializeField, Tooltip("コンボのテキストを表示させるTMPProUGUIを入れる")]
    private TextMeshProUGUI comboText;

    [Space(15)]

    /// <summary>
    /// 初期のコンボエフェクトデータ
    /// </summary>
    [SerializeField, Tooltip("初期のコンボエフェクトデータ")]
    private ComboEffectConfig resetConfig;

    /// <summary>
    /// 自身が属するオブジェクトプール
    /// </summary>
    private UIObjectPool<ComboEffectController> pool;

    /// <summary>
    /// 現在再生中のDOTweenシーケンス（途中でキャンセルするために保持）
    /// </summary>
    private Sequence activeSequence;

    #endregion


    #region 外部から呼び出し可能なエフェクト関連の関数

    /// <summary>
    /// オブジェクトプールから作成された際に呼ばれる関数
    /// </summary>
    /// <param name="pool">所属するプール</param>
    public void OnCreated(UIObjectPool<ComboEffectController> pool)
    {
        this.pool = pool;
    }

    /// <summary>
    /// コンボエフェクトの再生処理（表示、アニメーション、フェード）
    /// </summary>
    /// <param name="config">演出に使う設定（色や時間）</param>
    /// <param name="comboCount">現在のコンボ数</param>
    internal void Play(JudgementConfig config, int comboCount)
    {
        //スコアの値をログで表示
        DebugManager.Log($"[ScoreEffect] Play called: +{config.Logic.SetScoreValue}");

        // コンボテキストを表示状態にする
        comboText.gameObject.SetActive(true);

        // 既存のTweenを破棄して重複演出を防止
        comboText.transform.DOKill();
        comboText.DOKill();

        //現在再生中のDOTweenシーケンスが入っていれば
        if (activeSequence != null)
        {
            // 現在再生中のDOTweenシーケンスを停止する
            activeSequence.Kill();
            activeSequence = null;
        }

        //各種必要な設定を代入
        var visual = config.Visual;
        var comboCfg = visual.ComboEffect;

        // テキスト内容を更新（例：Combo: 5!）
        comboText.text = string.Format(comboCfg.ComboTextFormat, comboCount);

        // 表示色を設定（Visual設定から取得）
        comboText.color = visual.DisplayColor;
        comboText.alpha = comboCfg.StartAlpha;



        // 初期スケールを設定
        comboText.transform.localScale = comboCfg.StartScale;

        // アニメーションのシーケンスを作成
        activeSequence = DOTween.Sequence();

        activeSequence
            // スケールアニメーション　初期値から終了値の大きさに
            .Append(comboText.transform.DOScale(comboCfg.EndScale, visual.ScaleInTime).SetEase(visual.SetScaleEase))
            // 表示時間分待機
            .AppendInterval(visual.ShowDuration)
            // フェードアウト
            .Append(comboText.DOFade(comboCfg.EndAlpha, visual.FadeOutDuration))
            // 再利用処理（プールへ戻す）
            .OnComplete(ReturnToPool);
    }

    /// <summary>
    /// プールに戻す関数
    /// </summary>
    public void ReturnToPool()
    {
        //プールに戻す関数が呼ばれていることをログで表示
        DebugManager.Log("ReturnToPool called");

        //現在再生中のDOTweenシーケンスを停止、何も入っていない状態にする
        activeSequence?.Kill();
        activeSequence = null;

        //テキストの内容やアルファ値、スケール値を元に戻す
        comboText.text = string.Empty;
        comboText.alpha = resetConfig.StartAlpha;
        comboText.transform.localScale = resetConfig.StartScale;

        //テキストのオブジェクトを非表示に
        comboText.gameObject.SetActive(false);

        //プールに戻す
        pool?.Return(this);

    }

    #endregion

}


