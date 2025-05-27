using System;
using UnityEngine;

/// <summary>
/// 判定一つ分の設定情報のデータ
/// 判定名、許容タイミング、表示色、表示用アイコンを管理します。
/// </summary>
[System.Serializable]
public class JudgementConfig
{
    #region 判定の内部管理用変数
    [SerializeField, Header(" ロジック判定設定")]

    private JudgementLogicConfig logic;
    [SerializeField, Header(" 見た目・演出設定")]

    private JudgementVisualConfig visual;

    #endregion

    internal JudgementLogicConfig Logic { get { return logic; } }
    internal JudgementVisualConfig Visual { get { return visual; } }
    #region コンストラクタなど

    /// <summary>
    /// Fallback Miss を生成するための static factory
    /// </summary>
    /// <returns>JudgementConfig</returns>
    public static JudgementConfig CreateFallbackMiss()
    {
        return new JudgementConfig
        {
            logic = new JudgementLogicConfig
            {
                SetJudgementName = "Miss",
                SetBreaksCombo = true,
                SetMaxTimeDifference = 99f
                
            }


        };
    }

    /// <summary>
    /// フォールバック用コンストラクタ
    /// </summary>
    /// <param name="name">判定名</param>
    /// <param name="maxDiff"> 判定が成立する許容時間</param>
    /// <param name="col">判定の表示に使うカラー</param>
    /// <param name="icon">判定のアイコン画像</param>
    /// <param name="score">スコアの値</param>
    /// <param name="breakCom">コンボが途切れるかどうか</param>
    /// <param name="breakCom">コンボが途切れるかどうか</param>

    /// <summary>
    /// ロジック設定のみを初期化するコンストラクタ
    /// </summary>
    public JudgementConfig(string name, float maxDiff, int score, bool breakCom)
    {
        logic = new JudgementLogicConfig(name, maxDiff, score, breakCom);
        visual = new JudgementVisualConfig(); // デフォルト値で初期化
    }

    /// <summary>
    /// ビジュアル設定を設定するメソッド
    /// </summary>
    /// <param name="name">表示名</param>
    /// <param name="col">表示色</param>
    /// <param name="icon">表示アイコン（任意）</param>
    /// <param name="showTime">表示時間（デフォルト: 0.5f）</param>
    /// <param name="fadeTime">フェードアウト時間（デフォルト: 0.3f）</param>
    /// <returns>JudgementConfig（メソッドチェーン用）</returns>
    public JudgementConfig SetVisual(string name, Color col, Sprite icon = null, float showTime = 0.5f, float fadeTime = 0.3f)
    {
        visual = new JudgementVisualConfig(name, col, icon, showTime, fadeTime);
        return this; // メソッドチェーン用
    }


    // デフォルトコンストラクタ
    public JudgementConfig()
    {
        logic = new JudgementLogicConfig();
        visual = new JudgementVisualConfig();
    }
    /// <summary>
    /// JudgementConfigをNewする用
    /// </summary>

    #endregion
}