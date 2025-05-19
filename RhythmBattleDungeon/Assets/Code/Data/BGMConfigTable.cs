using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BGMConfig", menuName = "GameData/BGMConfigTable")]
/// <summary>
/// ゲーム内で使用するBGM設定の一覧を保持する ScriptableObject
/// </summary>
public class BGMConfigTable : ScriptableObject
{
    /// <summary>
    /// ゲーム内で使用するBGM設定の一覧のリスト
    /// </summary>
    [SerializeField, Header("ゲーム内で使用するBGM設定の一覧")]
    private List<BGMConfig> bgmList;

    /// <summary>
    /// BGMのディクショナリ
    /// </summary>
    private Dictionary<string, BGMConfig> bgmDict;


    /// <summary>
    ///リスト情報をすべて返す
    /// </summary>
    /// <returns></returns>
    internal List<BGMConfig> GetAll()
    {
        return bgmList;
    }

    /// <summary>
    /// リスト内のBGMConfigをIDで探して返す
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    internal BGMConfig GetBgmConfig(string id)
    {
        if (bgmDict == null)
        {
            InitializeDictionary();
        }

        bgmDict.TryGetValue(id, out var config);
        return config;
    }
    private void OnEnable()
    {
        // ScriptableObject 再読み込み時にも対応
        InitializeDictionary();
    }

    /// <summary>
    /// ディクショナリ初期化
    /// </summary>
    private void InitializeDictionary()
    {
        bgmDict = new Dictionary<string, BGMConfig>();
        foreach (var bgm in bgmList)
        {
            //BGMリストのBGMIDに文字列が入ってる＆ディクショナリにその文字列（キー）が含まれていないなら
            if (!string.IsNullOrEmpty(bgm.BgmId) && !bgmDict.ContainsKey(bgm.BgmId))
            {
               // ディクショナリにその文字列を追加
                bgmDict.Add(bgm.BgmId, bgm);
                foreach (var key in bgmDict.Keys)
                {
                    //どのキーが登録されているかのデバッグログ
                    Debug.Log($"登録されているBGMキー: {key}");
                }
            }
            else
            {
                //同じキーを登録しようとしているかBGMIDが空白
                Debug.LogWarning($"[BGMConfigTable] 重複または空のBGM ID: {bgm.BgmId}");
            }
        }
    }


}






