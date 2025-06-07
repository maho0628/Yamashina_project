
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ステージの設定データ
/// </summary>
[System.Serializable]
public class StageConfig
{
    #region  ステージ設定に関する情報変数

    /// <summary>
    /// ステージID名
    /// </summary>
    [SerializeField, Header("譜面ID名")]
    private string stageId;
    /// <summary>
    /// ステージで鳴らすBGMのID
    /// </summary>
    [SerializeField, Header("BGM ID")]
    private BGMName stageBgmId;

   
    private BGMConfigTable bgmTable;

    /// <summary>
    /// 譜面データJsonファイル名
    /// </summary>
    [SerializeField, Header("譜面データJsonファイル名")]
    private string chartFileName;

    /// <summary>
    /// ノーツのスクロール設定
    /// </summary>
    [SerializeField, Header("ノーツのスクロール設定")]
    private NoteScrollConfig scrollConfig;

    /// <summary>
    /// 判定設定（Perfect / Good / Miss など）
    /// </summary>
    [SerializeField, Header("判定設定（Perfect / Good / Miss など）")]
    private List<JudgementConfig> judgementConfigs;

    /// <summary>
    /// スコアゲージの演出・見た目設定
    /// </summary>
    [SerializeField, Header("スコアゲージの設定")]
    private GaugeConfig gaugeConfig;
    /// <summary>
    /// 楽曲終了後の遷移待機秒数
    /// </summary>
    [SerializeField, Header("楽曲終了後の遷移待機秒数")]
    private float delayBeforeResult = 2.0f;

  

   
    #endregion


    #region 読み取り専用プロパティ(ステージ設定に関する情報変数)

    /// <summary>
    /// 譜面ID名の読み取り専用
    /// </summary>
    internal string StageId => stageId;

    /// <summary>
    /// 譜面BGM音源の設定内容の読み取り専用
    /// </summary>
    internal BGMConfig StageBgm => bgmTable?.GetBgmConfig(stageBgmId);

    internal BGMName StageBgmId => stageBgmId;


    /// <summary>
    /// 譜面データJsonファイル名の読み取り専用
    /// </summary>
    internal string ChartFileName => chartFileName;

    /// <summary>
    /// ノーツのスクロール設定の読み取り専用
    /// </summary>
    internal NoteScrollConfig ScrollConfig => scrollConfig;

    /// <summary>
    ///  判定設定（Perfect / Good / Miss など）の読み取り専用
    /// </summary>
    internal List<JudgementConfig> JudgementConfigs => judgementConfigs;

    /// <summary>
    /// スコアゲージの設定の読み取り専用
    /// </summary>
    internal GaugeConfig GaugeConfig => gaugeConfig;
    /// <summary>
    /// 楽曲終了後の遷移待機秒数の読み取り専用
    /// </summary>
    internal float DelayBeforeResult => delayBeforeResult;

    internal void InitializeBGMTable(BGMConfigTable table)
    {
        this.bgmTable = table;
    }

    #endregion
}