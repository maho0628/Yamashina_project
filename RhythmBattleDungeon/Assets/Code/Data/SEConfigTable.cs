using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SEConfig", menuName = "GameData/SEConfigTable")]
/// <summary>
/// ゲーム内で使用するSE（効果音）の設定一覧を管理するScriptableObject
/// </summary>
public class SEConfigTable : ScriptableObject
{
    [SerializeField, Header("ゲーム内で使用するSE設定のリスト")]
    private List<SEConfig> seList;

    /// <summary>
    /// 指定したIDのSE設定を取得
    /// </summary>
    /// <param name="id">SEのID</param>
    /// <returns>対応するSEConfigデータ</returns>
    internal SEConfig GetSeConfig(string id)
    {
        return seList.Find(s => s.SeId == id);
    }
}

[System.Serializable]
/// <summary>
/// 単一のSE（効果音）の設定データ
/// </summary>
public class SEConfig
{
    [SerializeField, Header("SEのID名")]
    private string seId;

    [SerializeField, Header("使用するオーディオクリップ")]
    private AudioClip seAudioClip;

    [SerializeField, Header("SEの説明（任意）")]
    private string description;  // 例：「ボタン押下音」など

    // 以下は読み取り専用プロパティ
    internal string SeId => seId;
    internal AudioClip SeAudioClip => seAudioClip;
    internal string Description => description;
}
