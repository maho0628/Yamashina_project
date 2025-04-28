using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BGMConfig", menuName = "GameData/BGMConfigTable")]
// BGMを管理するためのスクリプタブルオブジェクト
public class BGMConfigTable : ScriptableObject
{
    //BGMの詳細設定のリスト
    [SerializeField] private List<BGMConfig> bgmsList;


    //シーンIDを探してBGMConfigデータを返すメソッド
    internal BGMConfig GetBgmConfig(string id)
    {
        return bgmsList.Find(s => s.SceneId == id);
    }

}

//BGMの詳細設定
[System.Serializable]
public class BGMConfig
{
    //スプレッドシートで管理するBGMのID名
    [SerializeField, Header("BGMのID名")]
    private string sceneId;

    //使う音源のオーディオクリップ
    [SerializeField, Header("使う音源のオーディオクリップ")]
    private AudioClip bgmAudioClip;

    [SerializeField, Header("使う音源のBPM")]
    private float bgmBpm;

    [SerializeField, Header("使う音源のジャンル名")]
    public string bgmGenre;//BGMのジャンル名　


    //シーン名を読み取りをする為のゲッター
    internal string SceneId => sceneId;
    //BGMのオーディオクリップの読み取りをする為のゲッター
    internal AudioClip BgmAudioClip => bgmAudioClip;

    //BGMのBPMの読み取りをするためのゲッター
    internal float BgmBpm => bgmBpm;

    //BGMのジャンル名の読み取りをするためのゲッター
    internal string BgmGenre => bgmGenre;

}