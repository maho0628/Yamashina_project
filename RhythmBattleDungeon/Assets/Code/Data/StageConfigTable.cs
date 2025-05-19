using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "GameData/Stage Config Table")]
/// <summary>
/// 各譜面ステージの設定を管理するScriptableObject
/// </summary>
public class StageConfigTable : ScriptableObject
{
    [SerializeField, Header("ステージ音源のリスト")]
    private List<StageConfig> stagesBgmList;

    /// <summary>
    /// 指定されたステージIDに対応するStageConfigデータを取得
    /// </summary>
    /// <param name="id">ステージのID</param>
    /// <returns>該当するStageConfigデータ、見つからない場合は null</returns>
    internal StageConfig GetStageConfig(string id)
    {
        var stageConfig = stagesBgmList.Find(s => s.StageId == id);
        if (stageConfig == null)
        {
            Debug.LogWarning($"ステージID '{id}' に対応するデータが見つかりません。");
        }
        return stageConfig;
    }


  

    /// <summary>
    /// 全ステージ設定を取得（曲一覧に使う）
    /// </summary>
    /// <returns></returns>
    /// 
    public List<StageConfig> GetAllStageConfigs()
    {
        return stagesBgmList;
    }

}

[System.Serializable]
/// <summary>
/// ステージ設定データ
/// </summary>
public class StageConfig
{
    [SerializeField, Header("ステージID名")]
    private string stageId;

    [SerializeField, Header("BGM音源の設定内容")]
    private BGMConfig stageBgm;

    [SerializeField, Header("譜面データJsonファイル名")]
    private string chartFileName;


    [SerializeField, Header("ノーツのスクロール設定")]
    private NoteScrollConfig scrollConfig;

    [SerializeField, Header("判定設定（Perfect / Good / Miss など）")]
    private List<JudgementConfig> judgementConfigs;
    // 以下はプロパティ

    /// <summary>
    /// ステージIDを取得
    /// </summary>
    internal string StageId => stageId;

    /// <summary>
    /// ステージに対応するBGM設定を取得
    /// </summary>
    internal BGMConfig StageBgm => stageBgm;

    /// <summary>
    /// 譜面データのファイル名を取得
    /// </summary>
    internal string ChartFileName => chartFileName;

    internal NoteScrollConfig ScrollConfig => scrollConfig;
    internal List<JudgementConfig> JudgementConfigs => judgementConfigs;
}
