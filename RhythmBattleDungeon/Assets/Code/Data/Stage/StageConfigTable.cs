using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "GameData/Stage Config Table")]
/// <summary>
//ステージ設定のScriptableObject
/// </summary>
public class StageConfigTable : ScriptableObject
{
    #region リストやディクショナリの内部管理用変数

    /// <summary>
    /// ステージ音源のリスト
    /// </summary>
    [Header("▼ステージ設定の一覧")]
    [SerializeField, Tooltip(" 各ステージの設定情報をまとめたリスト")]
    private List<StageConfig> stagesBgmLists;

    /// <summary>
    /// ゲーム内で使用するSE設定のリストのディクショナリ
    /// </summary>
    private Dictionary<string, StageConfig> stagesBgmDict;

    #endregion


    #region ゲッターメソッド

    /// <summary>
    /// 指定されたステージIDに対応するStageConfigデータを取得
    /// </summary>
    /// <param name="id">ステージのID</param>
    /// <returns>該当するStageConfigデータ</returns>

    internal StageConfig GetStageConfig(string id)
    {
        if (stagesBgmDict == null)
        {
            InitializeDictionary();
        }

        stagesBgmDict.TryGetValue(id, out var config);
        return config;
    }

    /// <summary>
    /// ステージに対応するBGMのリスト情報をすべて返す
    /// </summary>
    /// <returns>StageConfigデータ</returns>    
    internal List<StageConfig> GetAllStageConfigs()
    {
        return stagesBgmLists;
    }

    #endregion



    private void OnEnable()
    {
        // ScriptableObject 再読み込み時にも対応
        InitializeDictionary();
    }


    #region プライベートメソッド

    /// <summary>
    /// �f�B�N�V���i��������
    /// </summary>
    private void InitializeDictionary()
    {
        stagesBgmDict = new Dictionary<string, StageConfig>();
        foreach (var stageBgm in stagesBgmLists)
        {
            //ステージ音源のリストのStageIDに文字列が入ってる＆ディクショナリにその文字列（キー）が含まれていないなら
            if (!string.IsNullOrEmpty(stageBgm.StageId) && !stagesBgmDict.ContainsKey(stageBgm.StageId))
            {
                // ディクショナリにその文字列を追加
                stagesBgmDict.Add(stageBgm.StageId, stageBgm);
                foreach (var key in stagesBgmDict.Keys)
                {
                    //どのキーが登録されているかのデバッグログ
                    DebugManager.Log($"登録されているステージBGMキー: {key}");
                }
            }
            else
            {
                //同じキーを登録しようとしているかBGMIDが空白
                DebugManager.LogWarning($"[StageConfigTable] 重複または空のBGM ID: {stageBgm.StageId}");
            }
        }
    }

    #endregion

}





