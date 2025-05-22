using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "GameData/Stage Config Table")]
/// <summary>
/// 各譜面ステージの設定を管理するScriptableObject
/// </summary>
public class StageConfigTable : ScriptableObject
{
    #region リストやディクショナリ変数

    /// <summary>
    /// ステージ音源のリスト
    /// </summary>
    [SerializeField, Header("ステージ音源のリスト")]
    private List<StageConfig> stagesBgmLists;

    /// <summary>
    /// ゲーム内で使用するSE設定のリストのディクショナリ
    /// </summary>
    private Dictionary<string, StageConfig> stagesBgmDict;

    #endregion


    #region 読み取り専用プロパティ

    /// <summary>
    /// ゲーム内で使用するSE設定のリストの読み取り専用
    /// </summary>
    internal List<StageConfig> StagesBgmList => stagesBgmLists;

    #endregion


    #region ゲッターメソッド

    /// <summary>
    /// 指定されたステージIDに対応するStageConfigデータを取得
    /// </summary>
    /// <param name="id">ステージのID</param>
    /// <returns>StageConfigデータ</returns>
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
    /// 
    public List<StageConfig> GetAllStageConfigs()
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
    /// ディクショナリ初期化
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
                    Debug.Log($"登録されているステージBGMキー: {key}");
                }
            }
            else
            {
                //同じキーを登録しようとしているかBGMIDが空白
                Debug.LogWarning($"[StageConfigTable] 重複または空のBGM ID: {stageBgm.StageId}");
            }
        }
    }

    #endregion

}





