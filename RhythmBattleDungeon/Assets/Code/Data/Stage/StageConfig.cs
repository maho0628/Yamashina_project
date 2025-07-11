
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ステージの設定データ
/// </summary>
[System.Serializable]
public class StageConfig
{
    #region  ステージ設定の内部管理用変数

    /// <summary>
    /// ステージID名
    /// </summary>
    [Header("▼ ステージ基本情報")]

    [SerializeField, Tooltip("譜面ID名")]
    private string stageId;

    [Space(15)]

    /// <summary>
    /// 譜面BGMのID
    /// </summary>
    [SerializeField, Tooltip("譜面BGM ID")]
    private BGMName stageBgmId;

    [Space(15)]

    /// <summary>
    /// 譜面データJsonファイル名
    /// </summary>
    [SerializeField, Tooltip("譜面データJsonファイル名")]
    private string chartFileName;

    [Space(15)]

    /// <summary>
    /// ノーツのスクロール設定
    /// </summary>
    [SerializeField, Tooltip("ノーツのスクロール設定")]
    private NoteScrollConfig scrollConfig;

    [Space(15)]

    /// <summary>
    /// 判定設定（Perfect / Good / Miss など）
    /// </summary>
    [Header("▼ ステージ挙動・演出設定")]

    [SerializeField, Tooltip("判定設定（Perfect / Good / Miss など）")]
    private List<JudgementConfig> judgementConfigs;

    [Space(15)]

    /// <summary>
    /// 楽曲終了後の遷移待機秒数
    /// </summary>
    [Header("▼ その他")]

    [SerializeField, Tooltip("楽曲終了後の遷移待機秒数")]
    private float delayBeforeResult = 2.0f;

    /// <summary>
    /// ステージ設定内のBGMのテーブル
    /// </summary>
    private BGMConfigTable bgmTable;

    #endregion


    #region 読み取り専用プロパティ(ステージ設定の情報変数)

    /// <summary>
    /// 譜面ID名の読み取り専用
    /// </summary>
    internal string StageId => stageId;

    /// <summary>
    /// 譜面BGM音源の設定内容の読み取り専用
    /// </summary>
    internal BGMConfig StageBgm => bgmTable?.GetBgmConfig(stageBgmId);

    /// <summary>
    /// 譜面BGMのIDの読み取り専用
    /// </summary>
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
    /// 楽曲終了後の遷移待機秒数の読み取り専用
    /// </summary>
    internal float DelayBeforeResult => delayBeforeResult;

    #endregion


    #region ゲッター

    /// <summary>
    /// ステージ設定内のBGMのテーブルを返す
    /// </summary>
    /// <param name="table"></param>
    internal void GetStageBGMTable(BGMConfigTable table)
    {
        bgmTable = table;
    }

    #endregion

}