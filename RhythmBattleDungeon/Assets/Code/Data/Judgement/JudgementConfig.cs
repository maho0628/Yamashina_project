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

    /// <summary>
    /// ロジック判定設定
    /// </summary>
    [Header(" ロジック判定設定")]
    [SerializeField, Tooltip(" 各判定の内部で識別する名前や許容タイミングなどを設定します。")]
    private JudgementLogicConfig logic;

    /// <summary>
    /// 見た目・演出設定
    /// </summary>
    [Header(" 見た目・演出設定")]
    [SerializeField, Tooltip(" 各判定の表示名や判定のエフェクトを表示する際の各設定などを設定します。")]
    private JudgementVisualConfig visual;

    #endregion


    #region 読み取り専用プロパティ (判定の内部管理用変数)

    /// <summary>
    /// ロジック判定設定の読み取り専用
    /// </summary>
    internal JudgementLogicConfig Logic { get { return logic; } }

    /// <summary>
    /// 見た目・演出設定の読み取り専用
    /// </summary>
    internal JudgementVisualConfig Visual { get { return visual; } }

    #endregion


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
                ShouldBreakCombo = true,
                SetMaxTimeDifference = 99f,
                SetScoreValue = 0,


            },
          

                  


        };
    }


    // デフォルトコンストラクタ
    public JudgementConfig()
    {
        logic = new JudgementLogicConfig();
        visual = new JudgementVisualConfig();
    }


    #endregion
}