using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SEConfig", menuName = "GameData/SEConfigTable")]

/// <summary>
/// ゲーム内で使用するSE（効果音）の設定一覧を管理するScriptableObject
/// </summary>
public class SEConfigTable : ScriptableObject
{
    #region SEのリストやディクショナリ変数

    /// <summary>
    /// ゲーム内で使用するSE設定のリスト
    /// </summary>
    [SerializeField, Header("ゲーム内で使用するSE設定のリスト")]
    private List<SEConfig> seLists= new List<SEConfig>();

    /// <summary>
    /// ゲーム内で使用するSE設定のリストのディクショナリ
    /// </summary>
    private Dictionary<string, SEConfig> seConfigDict;

    #endregion


    #region 読み取り専用プロパティ

    /// <summary>
    /// ゲーム内で使用するSE設定のリストの読み取り専用
    /// </summary>
    internal List<SEConfig> SeLists => seLists;

    #endregion

    #region ゲッターメソッド

    /// <summary>
    /// ゲーム内で使用するSE設定のリスト情報をすべて返す
    /// </summary>
    /// <returns>SEConfigのリスト</returns>
    internal List<SEConfig> GetAllSeConfig()
    {
        return seLists;
    }

    /// <summary>
    /// リスト内のSEConfigをIDで探して返す
    /// </summary>
    /// <param name="id"></param>
    /// <returns>SEConfigのリスト</returns>
    internal SEConfig GetSeConfig(string id)
    {
        if (seConfigDict == null)
        {
            InitializeDictionary();
        }

        seConfigDict.TryGetValue(id, out var config);
        return config;
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
        seConfigDict = new Dictionary<string, SEConfig>();
        foreach (var se in seLists)
        {
            //SE設定の一覧のリストのSeIdに文字列が入ってる＆ディクショナリにその文字列（キー）が含まれていないなら
            if (!string.IsNullOrEmpty(se.SeId) && !seConfigDict.ContainsKey(se.SeId))
            {
                // ディクショナリにその文字列を追加
                seConfigDict.Add(se.SeId, se);
                foreach (var key in seConfigDict.Keys)
                {
                    //どのキーが登録されているかのデバッグログ
                    DebugManager.Log($"登録されているSEキー: {key}");
                }
            }
            else
            {
                //同じキーを登録しようとしているかJudgementNameが空白
                DebugManager.LogWarning($"[SEConfigTable] 重複または空のBGM ID: {se.SeId}");
            }
        }
    }

    #endregion


}


