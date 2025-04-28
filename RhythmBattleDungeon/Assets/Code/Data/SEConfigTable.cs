using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SEConfig", menuName = "GameData/SEConfigTable")]
// SEを管理するためのスクリプタブルオブジェクト
public class SEConfigTable : ScriptableObject
{
    // SEの詳細設定のリスト
    [SerializeField] private List<SEConfig> seList;

    // SE IDを探してSEConfigデータを返すメソッド
    internal SEConfig GetSeConfig(string id)
    {
        return seList.Find(s => s.SeId == id);
    }
}

// SEの詳細設定
[System.Serializable]
public class SEConfig
{
    [SerializeField, Header("SEのID名")]
    private string seId;

    [SerializeField, Header("使う音源のオーディオクリップ")]
    private AudioClip seAudioClip;

    [SerializeField, Header("SEの説明（任意）")]
    private string description;  // 例：「ボタン押下音」とか

    // ゲッター
    internal string SeId => seId;
    internal AudioClip SeAudioClip => seAudioClip;
    internal string Description => description;
}
