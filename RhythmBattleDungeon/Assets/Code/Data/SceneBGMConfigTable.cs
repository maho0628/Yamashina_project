using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// シーンに対応するBGM設定の一覧を保持する ScriptableObject
/// </summary>
public class SceneBGMConfigTable : ScriptableObject
{
    #region リストやディクショナリ変数

    /// <summary>
    /// シーンに対応するBGMのリスト
    /// </summary>
    [SerializeField, Header("シーンに対応するBGMのリスト")]
    private List<SceneBGMConfig> sceneBgmConfigLists= new List<SceneBGMConfig>();


    /// <summary>
    /// シーンに対応するBGMのリストのディクショナリ
    /// </summary>
    private Dictionary<string, string> sceneToBgmIdDict;

    #endregion


    #region 読み取り専用プロパティ

    /// <summary>
    /// シーンに対応するBGMのリストの読み取り専用
    /// </summary>
    internal List<SceneBGMConfig> SceneBgmConfigLists=> sceneBgmConfigLists;

    #endregion


    #region ゲッターメソッド

    /// <summary>
    /// シーンに対応するBGMのリスト情報をすべて返す
    /// </summary>
    /// <returns>SceneBGMConfigのList</returns>
    internal List<SceneBGMConfig> GetAllSceneBGMConfig()
    {
        return sceneBgmConfigLists;
    }

    /// <summary>
    /// sceneNameに対応したBGMIDを返す
    /// </summary>
    /// <param name="sceneName"></param>
    /// <returns>string</returns>
    internal string GetSceneBgmConfigName(string sceneName)
    {
        if (sceneToBgmIdDict == null)
        {
            InitializeDictionary();
        }

        sceneToBgmIdDict.TryGetValue(sceneName, out var bgmId);
        return bgmId;
    }

    #endregion



    private void OnEnable()
    {
        // ScriptableObject 再読み込み時にも対応
        InitializeDictionary();
    }


    #region プライベートメソッド

    /// <summary>
    /// ディクショナリの初期化
    /// </summary>
    private void InitializeDictionary()
    {
        sceneToBgmIdDict = new Dictionary<string, string>();
        foreach (var sceneBgm in sceneBgmConfigLists)
        {
            //コンフィグのシーンの名前とBgmIdが両方とも空欄でないなら
            if (!string.IsNullOrEmpty(sceneBgm.SceneName) && !string.IsNullOrEmpty(sceneBgm.BgmId))
            {
                //ディクショナリ内にSceneNameがないなら
                if (!sceneToBgmIdDict.ContainsKey(sceneBgm.SceneName))
                {
                    // ディクショナリ内にSceneNameとBgmIdを追加

                    sceneToBgmIdDict.Add(sceneBgm.SceneName, sceneBgm.BgmId);
                }
                else
                {
                    //エラー出して終了
                    Debug.LogWarning($"[SceneBGMConfigTable] シーン '{sceneBgm.SceneName}' は既に登録されています。");
                }
            }
        }
    }

    #endregion


}
